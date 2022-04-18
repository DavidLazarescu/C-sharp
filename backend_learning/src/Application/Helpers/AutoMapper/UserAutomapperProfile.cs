using AutoMapper;
using backend_learning.Infrastructure.DTOs.User;
using backend_learning.Domain.Entities;
using backend_learning.Infrastructure.DTOs.Account;


namespace backend_learning.Helpers.AutoMapper;

// An automapper profile defines mappings from one to another type and lets you change the proccess of mapping properties, if it doesnt follow the convention
// You need to create a mapping with "CreateMap" for every mapping you want to take place.
public class UserAutomapperProfile : Profile
{
    public UserAutomapperProfile()
    {
        // Create a mapping from "User" to "UserDto"
        CreateMap<User, UserOutputDto>()
            .ForMember(dest => dest.Jobs, temp => temp.MapFrom(src => src.Jobs.Select(p => p.Name)));  // Put only the names into "UserDto"'s "Jobs" property

        CreateMap<User, UserForUpdateDto>().ReverseMap();

        CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.SecretMessage, temp => temp.MapFrom(src => src.Message))
            .ForMember(dest => dest.TimeOfCreation, temp => temp.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Password, temp => temp.Ignore())
            .ForMember(dest => dest.Jobs, temp => temp.Ignore());
    }
}