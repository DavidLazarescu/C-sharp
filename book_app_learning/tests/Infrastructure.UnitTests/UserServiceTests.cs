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
using Domain.Entities;

namespace Tests.Infrastructure.UnitTests;

public class UserServiceTests : TestBase
{
    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 2)]
    [InlineData(-1, 2)]
    [InlineData(0, 2)]
    public async void AUserServiceGetsUsers(int pageNumber, int pageSize)
    {
        // Arrange
        var context = await createDatabaseContextAsync();
        var userService = new UserService(context, _mapper);
        var requestParameter = new UserRequestParameter { PageNumber = pageNumber, PageSize = pageSize };
        await DataContextSeeding.SeedDatabaseWithUsers(context);

        // Act
        var expectedResult = await context.Users
                                          .AsNoTracking()
                                          .Skip((requestParameter.PageNumber - 1) * requestParameter.PageSize)
                                          .Take(requestParameter.PageSize)
                                          .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync();

        var actualResult = (await userService.GetUsersAsync(requestParameter)).ToList();

        // Assert
        Assert.Equal(serializeToJson(expectedResult), serializeToJson(actualResult));
    }

    [Theory]
    [InlineData("KaktorDumbatz@gmail.com")]
    [InlineData("LukeRatatui@gmail.com")]
    [InlineData("SomethingNotExistent@troll.sdnbas")]
    public async void AUserServiceGetsUserByEmail(string email)
    {
        // Arrange
        var context = await createDatabaseContextAsync();
        var userService = new UserService(context, _mapper);
        await DataContextSeeding.SeedDatabaseWithUsers(context);

        // Act
        User user = await context.Users
                                 .AsNoTracking()
                                 .SingleOrDefaultAsync(user => user.Email == email);
        var expectedResult = _mapper.Map<UserDto>(user);

        var actualResult = await userService.GetUserByEmailAsync(email);

        // Assert
        Assert.Equal(serializeToJson(expectedResult), serializeToJson(actualResult));
    }
}