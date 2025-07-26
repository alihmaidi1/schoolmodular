using Autofac;
using Identity.Presentation;
using ILogger = Serilog.ILogger;

namespace Api.Modules;

public static class ModuleHelper
{

    public static async Task InitializeModules(string connectionString,ILogger  logger)
    {
        
        await IdentityStartup.Initialize(connectionString,logger);
        
    }

}