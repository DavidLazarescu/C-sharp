using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend_learning.DTOs;
using backend_learning.Entities;
using backend_learning.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend_learning.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<bool> ContainsUserWithEmail(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<IEnumerable<UserDto>> GetAllUserDtos()
        {
            return await _context.Users
                                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                .ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users
                                .Where(x => x.Email == email)
                                .SingleOrDefaultAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users
                                .Where(x => x.UserId == id)
                                .SingleOrDefaultAsync();
        }

        public async Task<UserDto> GetUserDtoByEmail(string email)
        {
            return await _context.Users
                                .Where(x => x.Email == email)
                                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                .SingleOrDefaultAsync();
        }

        public async Task<UserDto> GetUserDtoById(int id)
        {
            return await _context.Users
                                .Where(x => x.UserId == id)
                                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                .SingleOrDefaultAsync();
        }

        public async Task<bool> SaveChanges() => await _context.SaveChangesAsync() > 0;
    }
}