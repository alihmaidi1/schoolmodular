using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Application;
using Shared.Application.Message;
using Shared.Application.PiplineBehavior;
using Shared.Domain.Entities.Message;
using Shared.Domain.Event;

namespace Identity.Application;

public static class DependencyInjection
{

    public static IServiceCollection AddIdentityApplicationModules(this IServiceCollection services, IConfiguration configuration,Type DbContextType)
    {

        // services.AddMediatR(config =>
        // {
        //
        //     config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        //     config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        //     config.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
        //     config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        //     
        //
        // });
        //
        // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }

    

    public static WebApplication UseIdentityApplicationModule(this WebApplication app)
    {

        // app.UseIdentityInfrastructureModule();
        return app;
    }

}