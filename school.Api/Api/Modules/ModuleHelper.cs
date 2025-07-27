using Identity.Presentation;
namespace Api.Modules;

public static class ModuleHelper
{

    public static async Task InitializeModules(string connectionString,ILogger  logger,JwtSetting  jwtSetting)
    {
        
        await IdentityStartup.Initialize(connectionString,logger,jwtSetting);
        
    }

}