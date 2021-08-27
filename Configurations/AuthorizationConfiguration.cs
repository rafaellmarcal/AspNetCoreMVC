using AspNetCoreMVC.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreMVC.Configurations
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanStopApplication", policy => policy.RequireClaim("CanStopApplication"));

                options.AddPolicy("CanStart", policy => policy.Requirements.Add(new RequiredPermission("CanStart")));
                options.AddPolicy("CanRestart", policy => policy.Requirements.Add(new RequiredPermission("CanRestart")));
            });

            return services;
        }
    }
}
