using Xunit;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using backend_learning.Infrastructure;
using backend_learning.Domain.Entities;
using backend_learning.Application.Configuration;

namespace Application.UnitTests
{
    public class StartUpTests
    {
        private async Task<DataContext> createDatabaseContextAsync()
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


        [Fact]
        public async Task DatabaseIsSeededWithJobsAsync()
        {
            using var context = await createDatabaseContextAsync();

            Assert.Equal(0, await context.Jobs.CountAsync());
            await StartupConfiguration.SeedDatabaseAsync(context);
            Assert.Equal(3, await context.Jobs.CountAsync());
        }
    }
}