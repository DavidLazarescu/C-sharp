using Application.Dtos;
using Application.Interfaces.Services;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;

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


        public async Task<IEnumerable<UserDto>> GetAllUserDtos()
        {
            return await _context.Users.ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}