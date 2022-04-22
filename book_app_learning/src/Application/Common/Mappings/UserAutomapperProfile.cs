using Application.Common.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class UserAutomapperProfile : Profile
    {
        public UserAutomapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.AccountCreation, temp => temp.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Password, temp => temp.Ignore());
        }
    }
}