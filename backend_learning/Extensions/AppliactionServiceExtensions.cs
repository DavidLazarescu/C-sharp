using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend_learning.Data;
using backend_learning.Data.Repositories;
using backend_learning.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace backend_learning.Extensions
{
    public static class AppliactionServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddLogging();
            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });
            
            return services;
        }
    }
}