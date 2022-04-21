using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class UserAutomapperProfile : Profile
    {
        public UserAutomapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.AccountCreation, temp => temp.MapFrom(src => DateTime.UtcNow));
        }
    }
}