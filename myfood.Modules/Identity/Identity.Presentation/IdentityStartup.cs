using Autofac;
using Carter;
using Identity.Domain;
using Identity.infrastructure;
using Identity.infrastructure.Seed;
using Identity.Presentation.Message;
using Serilog;
using Shared.Application.Module;
using Shared.Infrastructure.Security.Jwt;

namespace Identity.Presentation;

public class IdentityStartup
{ 
    private static IContainer _container;


    public static async Task Initialize(string connectionString,ILogger logger,JwtSetting jwtSetting)
    {
        await ConfigureCompositionRoot(connectionString, logger,jwtSetting);
        await MessageStartup.InitializeMessageProcessors();


    }

    private static async Task ConfigureCompositionRoot(string connectionString,ILogger logger,JwtSetting jwtSetting)
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder.RegisterModule(new DataAccessModule(connectionString,jwtSetting));
        containerBuilder.RegisterModule(new MediatRModule(Application.AssemblyReference.Assembly));
        containerBuilder.RegisterModule(new MessageModule());
        containerBuilder.RegisterModule(new LoggingModule(logger));
        _container = containerBuilder.Build();
        IdentityCompositionRoot.SetContainer(_container);
        using(var scope= IdentityCompositionRoot.BeginLifetimeScope())
        {

            var serviceProvider = scope.Resolve<IServiceProvider>();
            IdentityDatabaseSeed.InitializeAsync(serviceProvider).GetAwaiter().GetResult();
        }

        


    }
}