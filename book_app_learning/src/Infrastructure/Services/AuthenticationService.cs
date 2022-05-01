using Application.Common.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Managers;
using Application.Common.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authenticationManager;


        public AuthenticationService(IMapper mapper, UserManager<User> userManager, IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
            _userManager = userManager;
            _mapper = mapper;
        }


        public async Task<string> AuthenticateUser(LoginDto loginDto)
        {
            if (!await _authenticationManager.ValidateUser(loginDto))
            {
                throw new InvalidParameterException("The provided login credentials are wrong");
            }

            return await _authenticationManager.CreateToken();
        }

        public async Task RegisterUser(RegisterDto registerDto)
        {
            var userExists = await _userManager.FindByEmailAsync(registerDto.Email);
            if (userExists != null)
            {
                throw new InvalidParameterException("A user with this email already exists");
            }

            var user = _mapper.Map<User>(registerDto);

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                throw new InvalidParameterException("The provided data was wrong");
            }

            await _userManager.AddToRolesAsync(user, registerDto.Roles);
        }
    }
}