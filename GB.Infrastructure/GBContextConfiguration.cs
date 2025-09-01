using GB.Application.Interfaces;
using GB.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GB.Infrastructure;

public static class GBContextConfiguration
{
    public static IServiceCollection AddGBContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
               b => b.MigrationsAssembly(typeof(GBContext).Assembly.FullName)));
        
        services.AddScoped<IGBContext,GBContext>();
        
        return services;
    }
}