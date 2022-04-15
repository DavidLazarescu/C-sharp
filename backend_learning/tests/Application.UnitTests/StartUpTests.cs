using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using backend_learning.Application.Configuration;
using backend_learning.TestBase;

namespace Application.UnitTests
{
    public class StartUpTests : TestBase
    {
        [Fact]
        public async Task DatabaseIsSeededWithJobsAsync()
        {
            using var context = await createDatabaseContextAsync();

            Assert.Equal(0, await context.Jobs.CountAsync());
            await StartupConfiguration.SeedDatabaseWithJobsAsync(context);
            Assert.Equal(3, await context.Jobs.CountAsync());
        }
    }
}