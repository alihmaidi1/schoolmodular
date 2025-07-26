using Autofac;
using Carter;
using Identity.Domain;
using Identity.infrastructure;
using Identity.Presentation.Message;
using Serilog;

namespace Identity.Presentation;

public class IdentityStartup
{ 
    private static IContainer _container;


    public static async Task Initialize(string connectionString,ILogger logger)
    {
        await ConfigureCompositionRoot(connectionString, logger);
        await MessageStartup.InitializeMessageProcessors();


    }

    private static async Task ConfigureCompositionRoot(string connectionString,ILogger logger)
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder.RegisterModule(new DataAccessModule(connectionString));
        containerBuilder.RegisterModule(new MediatRModule());
        containerBuilder.RegisterModule(new MessageModule());
        containerBuilder.RegisterModule(new LoggingModule(logger));

        
        
        _container = containerBuilder.Build();
        IdentityCompositionRoot.SetContainer(_container);
        


    }
}