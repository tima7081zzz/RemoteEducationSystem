using Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Configuration;

namespace Persistence.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services
            .AddScoped<IConnectionManager, ConnectionManager>();

        return services;
    }
}