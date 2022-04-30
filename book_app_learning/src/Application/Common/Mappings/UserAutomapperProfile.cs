using System.Text;
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
                .ForMember(dest => dest.AccountCreation, temp => temp.MapFrom(src => DateTime.UtcNow));
            
            CreateMap<User, UserUpdateDto>().ReverseMap();
        }
    }
}