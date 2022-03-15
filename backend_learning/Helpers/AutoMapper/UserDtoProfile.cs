using backend_learning.Entities;
using backend_learning.DTOs;
using AutoMapper;
using System.Security.Cryptography;
using System.Text;


namespace backend_learning.Helpers.AutoMapper
{
    public class UserDtoProfile : Profile
    {
        public UserDtoProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Age, temp => temp.MapFrom(src => src.Age - 1));

            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.Password, temp => temp.MapFrom(src => Hash(src.Password)));
        }

        private string Hash(string toHash)
        {
            var hmac = new HMACSHA256();
            var result = hmac.ComputeHash(Encoding.UTF8.GetBytes(toHash));

            return result.ToString();
        }
    }
}