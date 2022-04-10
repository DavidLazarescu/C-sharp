using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend_learning.Infrastructure.DTOs;
using backend_learning.Domain.Entities;
using backend_learning.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend_learning.Infrastructure.Repositories
{
    // The repository pattern implements a layer of abstraction between the classes which want to access the database and
    // EF core. You are able to write cleaner and more understandable code, by not writing the LINQ queries yourself, but
    // calling methods which do what you want 
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
                                .Include(p => p.Jobs)
                                .Where(x => x.Email == email)
                                .SingleOrDefaultAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users
                                .Include(p => p.Jobs)
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

        public async Task AddUser(User user) => await _context.AddAsync(user);

        public async Task<bool> SaveChanges() => await _context.SaveChangesAsync() > 0;
    }
}