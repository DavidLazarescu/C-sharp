using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using backend_learning.Data;
using backend_learning.Data.Repositories;
using backend_learning.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;


namespace backend_learning.Extensions
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