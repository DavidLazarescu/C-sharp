using backend_learning.Domain.Entities;
using backend_learning.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace backend_learning.Application.Configuration;

// Extension method for things which should happen during startup
public static class StartupConfiguration
{
    public static void AdditionalConfiguration(IServiceProvider services)
    {
        var loggerFactory = services.GetService<ILoggerFactory>();
        loggerFactory.AddFile(Directory.GetCurrentDirectory() + "/Data/Logs/");  // Configure that the logger should log to the specified file
    }

    public async static Task SeedDatabaseWithJobsAsync(DataContext context)
    {
        if (!context.Jobs.Any())
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
    }
}