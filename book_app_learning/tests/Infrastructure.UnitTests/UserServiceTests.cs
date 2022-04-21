using Infrastructure.Persistance.Seeding;
using Infrastructure.Services;
using Infrastructure.UnitTests;
using AutoMapper.QueryableExtensions;
using Xunit;
using Application.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using Application.Common.RequestParameters;
using System;

namespace Tests.Infrastructure.UnitTests;

public class UserServiceTests : TestBase
{
    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 2)]
    [InlineData(-1, 2)]
    [InlineData(0, 2)]
    public async void GetsAllusers(int pageNumber, int pageSize)
    {
        // Arrange
        var context = await createDatabaseContextAsync();
        var userService = new UserService(context, _mapper);
        var requestParameter = new UserRequestParameter { PageNumber = pageNumber, PageSize = pageSize };

        // Act
        await DataContextSeeding.SeedDatabaseWithUsers(context);
        var expectedResult = await context.Users
                                          .AsNoTracking()
                                          .Skip((requestParameter.PageNumber - 1) * requestParameter.PageSize)
                                          .Take(requestParameter.PageSize)
                                          .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync();

        var actualResult = (await userService.GetUsers(requestParameter)).ToList();


        // Assert
        Assert.Equal(JsonConvert.SerializeObject(expectedResult), JsonConvert.SerializeObject(actualResult));
    }
}