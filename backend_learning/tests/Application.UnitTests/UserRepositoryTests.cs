using Xunit;
using backend_learning.TestBase;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using backend_learning.Infrastructure.Repositories;
using backend_learning.Infrastructure.DTOs.User;
using backend_learning.Domain.Entities;
using AutoMapper.QueryableExtensions;


namespace Application.UnitTests;

public class UserRepositoryTests : TestBase
{
    [Fact]
    public async Task FindsAlreadyExistingUserAsync()
    {
        // Arange
        using var context = await createDatabaseContextAsync();
        await seedDatabaseWith5UsersAsync(context);
        var userRepository = new UserRepository(context, _mapper);

        // Act
        string existingEmail = (await context.Users.FirstAsync()).Email;
        bool userExists = await userRepository.UserAlreadyExistsAsync(existingEmail);

        // Assert
        Assert.True(userExists);
    }

    [Fact]
    public async Task DoesntFindNonExistenUserAsync()
    {
        // Arrange
        using var context = await createDatabaseContextAsync();
        await seedDatabaseWith5UsersAsync(context);
        var userRepository = new UserRepository(context, _mapper);

        // Act
        bool userExists = await userRepository.UserAlreadyExistsAsync("nonExistentEmail@gmail.com");

        // Assert
        Assert.False(userExists);
    }

    [Fact]
    public async Task GetsAllUserDtosAsync()
    {
        // Arrange
        using var context = await createDatabaseContextAsync();
        await seedDatabaseWith5UsersAsync(context);
        var userRepository = new UserRepository(context, _mapper);

        // Act
        List<UserOutputDto> userDtos = (await userRepository.GetAllUserDtosAsync(trackChanges: false)).ToList();
        int amountOfDtos = userDtos.Count;

        // Assert
        Assert.Equal(5, amountOfDtos);
    }

    [Fact]
    public async Task GetsAllUserDtosWithCorrectContentAsync()
    {
        // Arrange
        using var context = await createDatabaseContextAsync();
        await seedDatabaseWith5UsersAsync(context);
        var userRepository = new UserRepository(context, _mapper);

        // Act
        List<User> users = await context.Users.ToListAsync();
        List<UserOutputDto> userDtos = (await userRepository.GetAllUserDtosAsync(trackChanges: false)).ToList();

        // Assert
        Assert.Equal(users[0].Name, userDtos[0].Name);
        Assert.Equal(users[0].Email, userDtos[0].Email);
        Assert.Equal(users[0].Age, userDtos[0].Age);

        Assert.Equal(users[1].Name, userDtos[1].Name);
        Assert.Equal(users[1].Email, userDtos[1].Email);
        Assert.Equal(users[1].Age, userDtos[1].Age);

        Assert.Equal(users[4].Name, userDtos[4].Name);
        Assert.Equal(users[4].Email, userDtos[4].Email);
        Assert.Equal(users[4].Age, userDtos[4].Age);
    }

    [Fact]
    public async Task GetsUserByEmailAsync()
    {
        // Arrange
        using var context = await createDatabaseContextAsync();
        await seedDatabaseWith5UsersAsync(context);
        await seedDatabaseWith3JobsAsync(context);
        var userRepository = new UserRepository(context, _mapper);

        // Act
        User user = await context.Users.Include(p => p.Jobs).FirstAsync();

        // Assert
        Assert.Equal(user, await userRepository.GetUserByEmailAsync(user.Email, trackChanges: true));
    }

    [Fact]
    public async Task GetsUserByIdAsync()
    {
        // Arrange
        using var context = await createDatabaseContextAsync();
        await seedDatabaseWith5UsersAsync(context);
        await seedDatabaseWith3JobsAsync(context);
        var userRepository = new UserRepository(context, _mapper);

        // Act
        User user = await context.Users.Include(p => p.Jobs).FirstAsync();

        // Assert
        Assert.NotStrictEqual(user, await userRepository.GetUserByIdAsync(user.UserId, trackChanges: false));
    }

    [Fact]
    public async Task GetsUserDtoByEmailAsync()
    {
        // Arrange
        using var context = await createDatabaseContextAsync();
        await seedDatabaseWith5UsersAsync(context);
        await seedDatabaseWith3JobsAsync(context);
        var userRepository = new UserRepository(context, _mapper);

        // Act
        UserOutputDto userDto = await context.Users
                                    .ProjectTo<UserOutputDto>(_mapper.ConfigurationProvider)
                                    .FirstAsync();

        // Assert
        Assert.NotStrictEqual(userDto, await userRepository.GetUserDtoByEmailAsync(userDto.Email, trackChanges: true));
    }
}