using backend_learning.Entities;
using backend_learning.DTOs;
using AutoMapper;


namespace backend_learning.Helpers.AutoMapper
{
    // A Automapper profile. It specifies how conversions between properties should be, which do not follow the convention.
    public class UserDtoProfile : Profile
    {
        public UserDtoProfile()
        {
            CreateMap<User, UserDto>()  // Conversion from User to UserDto
                .ForMember(dest => dest.Age, temp => temp.MapFrom(src => src.Age - 1));  //Remove one year of the age when converting
        }
    }
}