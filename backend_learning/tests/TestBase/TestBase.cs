using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using backend_learning.Infrastructure;
using backend_learning.Application.Helpers.AutoMapper;
using backend_learning.Domain.Entities;



namespace backend_learning.TestBase
{
    public class TestBase
    {
        protected Mapper _mapper;


        public TestBase()
        {
            var config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(JobAutomapperProfile).Assembly));
            _mapper = new Mapper(config);
        }


        protected async Task<DataContext> createDatabaseContextAsync()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(connection)
                .Options;

            var context = new DataContext(contextOptions);
            await context.Database.EnsureCreatedAsync();

            return context;
        }

        protected async Task seedDatabaseWith3JobsAsync(DataContext context)
        {
            context.Jobs.Add(new Job
                {
                    Name = "Programmer",
                    Description = "Typing stuff",
                    Location = "Remote",
                    YearlySalary = 80000
                });

                context.Jobs.Add(new Job
                {
                    Name = "Construction worker",
                    Description = "Building stuff",
                    Location = "NYC",
                    YearlySalary = 50000
                });

                context.Jobs.Add(new Job
                {
                    Name = "Writer",
                    Description = "Writes books",
                    Location = "Remote",
                    YearlySalary = 60000
                });

                await context.SaveChangesAsync();
        }

        protected async Task seedDatabaseWith5UsersAsync(DataContext context)
        {
            var names = new List<string> { "David", "Giulia", "Knotti", "Maritz", "Yusef" };
            for (int i = 0; i < names.Count; ++i)
            {
                await context.Users.AddAsync(new User
                {
                    Age = 12,
                    Name = names[i],
                    Email = $"{names[i].ToLower()}@gmail.com",
                    Password = new byte[0],
                    Jobs = {},
                    PasswordSeed = new byte[0],
                    SecretMessage = "This is just a test",
                    TimeOfCreation = DateTime.UtcNow
                });
            }

            await context.SaveChangesAsync();
        }
    }
}