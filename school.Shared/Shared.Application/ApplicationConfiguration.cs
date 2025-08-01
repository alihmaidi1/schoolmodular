using System.Reflection;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.CQRS;
using Shared.Application.Extensions;
using Shared.Application.Message;
using Shared.Application.PiplineBehavior;
using Shared.Application.Services.User;
using Shared.Application.Versioning;
using Shared.Domain.Entities.Message;
using Shared.Domain.Event;

namespace Shared.Application;

public static class ApplicationConfiguration
{

    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        Dictionary<Type, Assembly> assemblies)
    {

        #region Core
        services.AddVersioning();
        services.AddMemoryCache();
        services.AddCarterWithAssemblies(assemblies.Values.ToArray());

        #endregion        
        
        #region CQRS_Abstraction
        

        services.AddSingleton<IEventDispatcher, EventDispatcher>();
        foreach (var assembly in assemblies)
        {

            services.AddEventHandlersWithDecorate<OutboxMessageConsumer,IDomainEvent>(assembly.Value,assembly.Key);
            services.AddEventHandlersWithDecorate<OutboxMessageConsumer,IIntegrationEvent>(assembly.Value,assembly.Key);

            
        }
        
        #endregion

        #region Services

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService,CurrentUserService>();


        #endregion

        return services;
    }


    public static WebApplication UseApplication(this WebApplication app)
    {
        
        
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "");
        
        });
        app.MapCarter();
        
        return app;
    }
}