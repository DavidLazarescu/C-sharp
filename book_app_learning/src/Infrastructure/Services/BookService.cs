using Application.Common.Dtos;
using Application.Common.Interfaces.Services;
using Application.Common.RequestParameters;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BookService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<IEnumerable<BookOutDto>> GetBooks(BookRequestParameter requestParameter)
        {
            if(!requestParameter.IsValid)
            {
                // Give error back
            }

            return await _context.Books
                                 .AsNoTracking()
                                 .Skip((requestParameter.PageNumber - 1) * requestParameter.PageSize)
                                 .Take(requestParameter.PageSize)
                                 .ProjectTo<BookOutDto>(_mapper.ConfigurationProvider)
                                 .ToListAsync();
        }
    }
}