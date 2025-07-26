using Autofac;
using Autofac.Extensions.DependencyInjection;
using Identity.Domain;
using Identity.infrastructure.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddIdentityInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
    {
        
        
        return services;
    }

    
    
    public static WebApplication UseIdentityInfrastructureModule(this WebApplication app)
    {
        using(var scope= IdentityCompositionRoot.BeginLifetimeScope())
        {

            var serviceProvider = scope.Resolve<IServiceProvider>();
            IdentityDatabaseSeed.InitializeAsync(serviceProvider).GetAwaiter().GetResult();
        }

        return app;
    }

}