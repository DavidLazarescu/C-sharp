using backend_learning.Domain.Entities;
using backend_learning.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace backend_learning.Application.Extensions
{
    // Extension method for things which should happen during startup
    public static class StartupExtensions
    {
        public static WebApplication AdditionalConfiguration(this WebApplication app, IServiceProvider services)
        {
            var loggerFactory = services.GetService<ILoggerFactory>();
            loggerFactory.AddFile(Directory.GetCurrentDirectory() + "/Data/Logs/");  // Configure that the logger should log to the specified file

            return app;
        }

        public static WebApplication SeedDatabase(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetService<DataContext>();

            if(!context.Jobs.Any())
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

                context.SaveChanges();
            }
            

            return app;
        }
    }
}