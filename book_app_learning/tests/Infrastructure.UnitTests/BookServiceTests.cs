using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.Common.RequestParameters;
using AutoMapper.QueryableExtensions;
using Infrastructure.Persistance.Seeding;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;

namespace Infrastructure.UnitTests
{
    public class BookServiceTests : TestBase
    {
        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 2)]
        public async Task ABookServiceGetsBooks(int pageNumber, int pageSize)
        {
            // Arrange
            var context = await createDatabaseContextAsync();
            var bookService = new BookService(context, _mapper);
            var requestParameter = new BookRequestParameter { PageNumber = pageNumber, PageSize = pageSize };
            await DataContextSeeding.SeedDatabaseWithUsers(context);

            // Act
            var expectedResult = await context.Books
                                              .AsNoTracking()
                                              .Skip((requestParameter.PageNumber - 1) * requestParameter.PageSize)
                                              .Take(requestParameter.PageSize)
                                              .ProjectTo<BookOutDto>(_mapper.ConfigurationProvider)
                                              .ToListAsync();

            var actualResult = (await bookService.GetBooks(requestParameter)).ToList();

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(expectedResult), JsonConvert.SerializeObject(actualResult));
        }
    }
}