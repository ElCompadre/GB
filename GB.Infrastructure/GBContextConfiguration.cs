using GB.Application.Interfaces;
using GB.Application.Interfaces.Repositories;
using GB.Infrastructure.Context;
using GB.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GB.Infrastructure;

public static class GBContextConfiguration
{
    public static IServiceCollection AddGBContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IGBContext, GBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
               b => b.MigrationsAssembly(typeof(GBContext).Assembly.FullName)));
        
        services.AddScoped<IBrasserieRepository, BrasserieRepository>();
        services.AddScoped<IBiereRepository, BiereRepository>();
        services.AddScoped<IGrossisteRepository, GrossisteRepository>();
        services.AddScoped<IGrossisteBiereRepository, GrossisteBiereRepository>();
        return services;
    }
}