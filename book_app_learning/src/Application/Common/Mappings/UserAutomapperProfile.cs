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
                .ForMember(dest => dest.AccountCreation, temp => temp.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Password, temp => temp.Ignore());
            
            CreateMap<User, UserUpdateDto>()
                .ForMember(dest => dest.Password, temp => temp.MapFrom(src => Encoding.Default.GetString(src.Password)));

            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.Password, temp => temp.MapFrom(src => Encoding.UTF8.GetBytes(src.Password)));
        }
    }
}