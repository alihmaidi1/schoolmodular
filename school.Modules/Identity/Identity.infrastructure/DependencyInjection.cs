using System.Reflection;
using Identity.Domain.Repository;
using Identity.Domain.Security;
using Identity.infrastructure.Repositories;
using Identity.infrastructure.Repositories.Jwt;
using Identity.infrastructure.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Domain.Entities.Message;
using Shared.Domain.Event;
using Shared.Infrastructure.Database;
using Shared.Infrastructure.Messages;

namespace Identity.infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddIdentityInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
    {


        services.AddScoped<IJwtRepository, JwtRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddIdentityCore<User>(option =>
            {
                option.SignIn.RequireConfirmedAccount = true;
                
            
            
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<schoolIdentityDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders()
            .AddApiEndpoints();

        services.AddDbContext<schoolIdentityDbContext>(Postgres.StandardOptions(configuration, Schemas.Identity));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        // AddDomainEventHandlers(services);
        // services.AddDomainEventHandlers(Assembly.GetExecutingAssembly());
        // var IeventHandlerType = Assembly.GetExecutingAssembly()
        //     .GetTypes()
        //     .Where(t => t.GetInterfaces()
        //         .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>)))
        //     .ToList();
        //
        //
            // typeof(IEventHandler<>).Assembly;
        // services.TryDecorate(typeof(IEventHandler<>),typeof(IdempotentIdentityOutboxEventHandler<>));
        // services.TryDecorate(typeof(IEventHandler<>),typeof(IdempotentIdentityInboxEventHandler<>),Assembly.GetExecutingAssembly());

        return services;
    }

    
    
    public static WebApplication UseIdentityInfrastructureModule(this WebApplication app)
    {
    
        using(var scope= app.Services.CreateScope()){
        
            IdentityDatabaseSeed.InitializeAsync(scope.ServiceProvider).GetAwaiter().GetResult();
        }

        return app;
    }

}