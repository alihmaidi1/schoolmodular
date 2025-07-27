using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Refit;
using Shared.Domain.Entities.Message;
using Shared.Domain.Event;
using Shared.Domain.Services.Email;
using Shared.Domain.Services.Twilio;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Messages;
using Shared.Infrastructure.Messages.Outbox;
using Shared.Infrastructure.Security;
using Shared.Infrastructure.Security.Jwt;
using Shared.Infrastructure.Services.Email;
using Shared.Infrastructure.Services.Twilio;
using Shared.Infrastructure.Services.Whatsapp;

namespace Shared.Infrastructure;

public static class InfrastructureConfiguration
{


    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,List<Type>  dbContexts)
    {

        services.AddLimitRate();
        services.AddJwtConfiguration(configuration);
        
        services.AddOptions<JwtSetting>()
            .BindConfiguration("Jwt")
            .ValidateDataAnnotations()
            .ValidateOnStart();
    
    
        services.TryAddSingleton<InsertOutboxMessagesInterceptor>();
                
        return services;
    }
    
    
    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseRateLimiter();
        //
        app.UseAuthentication();
   
        

        return app;
    }

}