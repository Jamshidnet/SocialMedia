using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace SocialMedia.Application;

public static  class ConfigurationService 
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}
