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
            {
                throw new InvalidParameterException("Request parameters are invalid");
            }

            return await _context.Users
                                 .AsNoTracking()
                                 .Skip((requestParameter.PageNumber - 1) * requestParameter.PageSize)
                                 .Take(requestParameter.PageSize)
                                 .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                 .ToListAsync();
        }

        public async Task RegisterUserAsync(RegisterDto registerDto)
        {
            if(_context.Users.Any(user => user.Email == registerDto.Email))
            {
                throw new InvalidParameterException("A user with this email already exists");
            }

            User user = _mapper.Map<User>(registerDto);
            
            var hashedPasswordAndKey = GeneratePasswordHashAndKey(registerDto.Password);
            user.Password = hashedPasswordAndKey.Item1;
            user.PasswordKey = hashedPasswordAndKey.Item2;

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        private ValueTuple<byte[], byte[]> GeneratePasswordHashAndKey(string password)
        {
            var hmac = new HMACSHA256();
            return (hmac.ComputeHash(Encoding.UTF8.GetBytes(password)), hmac.Key);
        }
    }
}