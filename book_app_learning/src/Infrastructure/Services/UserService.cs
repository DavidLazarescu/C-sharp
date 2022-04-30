using Application.Common.Dtos;
using Application.Interfaces.Services;
using Application.Common.RequestParameters;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Domain.Entities;
using System.Security.Cryptography;
using System.Text;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        

        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            User user = await _context.Users
                                      .AsNoTracking()
                                      .SingleOrDefaultAsync(user => user.Email == email);
            
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync(UserRequestParameter requestParameter)
        {
            if(!requestParameter.IsValid)
                throw new InvalidParameterException("Request parameters are invalid");

            return await _context.Users
                                 .AsNoTracking()
                                 .Skip((requestParameter.PageNumber - 1) * requestParameter.PageSize)
                                 .Take(requestParameter.PageSize)
                                 .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                 .ToListAsync();
        }

        public async Task PatchUserAsync(string email, JsonPatchDocument<UserUpdateDto> patchDoc, ControllerBase controllerBase)
        {
            User user = await _context.Users.SingleOrDefaultAsync(user => user.Email == email);

            if(user == null)
                throw new InvalidParameterException("No user with this emails address exists");


            var userToPatch = _mapper.Map<UserUpdateDto>(user);

            patchDoc.ApplyTo(userToPatch, controllerBase.ModelState);
            controllerBase.TryValidateModel(controllerBase.ModelState);
            
            if(!controllerBase.ModelState.IsValid)
                throw new InvalidParameterException("The provided user update data is invalid");


            _mapper.Map(userToPatch, user);
            
            await _context.SaveChangesAsync();
        }

        private ValueTuple<byte[], byte[]> GeneratePasswordHashAndKey(string password)
        {
            var hmac = new HMACSHA256();
            return (hmac.ComputeHash(Encoding.UTF8.GetBytes(password)), hmac.Key);
        }
    }
}