using Application.Common.Dtos;
using Application.Interfaces.Services;
using Application.Common.RequestParameters;
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


        public async Task<IEnumerable<UserDto>> GetUsers(UserRequestParameter requestParameter)
        {
            if(!requestParameter.IsValid)
            {
                // Give error back
            }

            return await _context.Users
                                 .AsNoTracking()
                                 .Skip((requestParameter.PageNumber - 1) * requestParameter.PageSize)
                                 .Take(requestParameter.PageSize)
                                 .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                 .ToListAsync();
        }
    }
}