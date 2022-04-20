using Infrastructure;
using Infrastructure.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAppUserRepository, AppUserRepository>();
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}