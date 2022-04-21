using Application.Common.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class BookAutomapperProfile : Profile
    {
        public BookAutomapperProfile()
        {
            CreateMap<Book, BookOutDto>()
                .ForMember(dest => dest.Authors, temp => temp.MapFrom(src => src.Authors.Select(author => author.FirstName)));
        }
    }
}