using System.Reflection;
using Mapster;
using MapsterMapper;

namespace BuberDinner.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddSingleton<IMapper, ServiceMapper>();

        return services;
    }
}