using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Infrastructure.Messages;

public static class MassTransitExtentions
{
    public static IServiceCollection AddMassTransitWithAssemblies
        (this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            config.AddConsumers(assemblies);
            config.AddActivities(assemblies);
            
            config.UsingInMemory((context, configurator) =>configurator.ConfigureEndpoints(context));
            
        });
        // services.TryDecorate(typeof(IConsumer<>), typeof(IdempotencyIntegrationEventHandler<,>));
        return services;
    }

    
}