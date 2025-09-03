using GB.Application.Interfaces.Services;
using GB.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GB.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBrasserieService, BrasserieService>();
        services.AddScoped<IBiereService, BiereService>();
        services.AddScoped<IGrossisteService, GrossisteService>();
        return services;
    }
}