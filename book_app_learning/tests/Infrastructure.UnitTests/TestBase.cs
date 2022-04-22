using System;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using AutoMapper;
using Infrastructure.Persistance;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Infrastructure.UnitTests
{
    public class TestBase
    {
        protected IUserService _userService;
        protected Mapper _mapper;


        public TestBase()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));
            _mapper = new Mapper(mapperConfig);
        }

        protected async Task<DataContext> createDatabaseContextAsync()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            await connection.OpenAsync();

            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(connection)
                .Options;

            var context = new DataContext(contextOptions);
            await context.Database.EnsureCreatedAsync();

            return context;
        }

        protected string serializeToJson(object toSerialize)
        {
            return JsonConvert.SerializeObject(toSerialize);
        }
    }
}