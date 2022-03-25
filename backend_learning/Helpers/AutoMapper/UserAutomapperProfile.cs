using AutoMapper;
using backend_learning.DTOs;
using backend_learning.Entities;

namespace backend_learning.Helpers.AutoMapper
{
    // An automapper profile defines mappings from one to another type and lets you change the proccess of mapping properties, if it doesnt follow the convention
    // You need to create a mapping with "CreateMap" for every mapping you want to take place.
    public class UserAutomapperProfile : Profile
    {
        public UserAutomapperProfile()
        {
            // Create a mapping from "User" to "UserDto"
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Jobs, temp => temp.MapFrom(src => src.Jobs.Select(p => p.Name)));  // Put only the names into "UserDto"'s "Jobs" property
        }
    }
}