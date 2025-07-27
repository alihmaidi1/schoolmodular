using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Shared.Domain.Event;
using Shared.Infrastructure.Database;

namespace Notification;




public static class NotificationModule
{
    
    public static IServiceCollection AddNotificationModule(this IServiceCollection services,
        IConfiguration configuration)
    {


        services.AddSignalR(options =>
        {
            
            options.EnableDetailedErrors = true;
            
        });
        services.AddSingleton<IUserIdProvider, UserIdProvider>();
        
        services.AddDbContext<schoolNotificationDbContext>(Postgres.StandardOptions(configuration, Schemas.Notification));
        
        return services;

    }

    public static WebApplication UseNotificationModule(this WebApplication app)
    {

        
        return app;
    }
    
}