using System.Security.Cryptography;
using backend_learning.Data.Repositories;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;

namespace backend_learning.Extensions
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
    }
}