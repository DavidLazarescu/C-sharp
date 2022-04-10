using System.Reflection;
using backend_learning.Infrastructure;
using backend_learning.Infrastructure.Interfaces;
using backend_learning.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace backend_learning.Application.Extensions
{
    // A class for extensions to the application's services. We are extracting them into an extension method, so they
    // dont clutter up the "Program.cs" file
    public static class ServiceExtensions
    {
        // The extension-method which adds the services
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddLogging();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });
            
            return services;
        }
    }
}