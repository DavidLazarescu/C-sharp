using Application.Common.Dtos;
using Application.Common.RequestParameters;

namespace Application.Common.Interfaces.Services
{
    public interface IBookService
    {
        public Task<IEnumerable<BookOutDto>> GetBooks(BookRequestParameter requestParameter);
    }
}