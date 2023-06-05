using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Application.Abstraction;
using SocialMedia.Infrustructure.Interceptor;

namespace SocialMedia.Infrustructure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString: configuration.GetConnectionString("DbConnect"));
            });

            services.AddScoped<InterceptorClass>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            return services;
        }

    }
}