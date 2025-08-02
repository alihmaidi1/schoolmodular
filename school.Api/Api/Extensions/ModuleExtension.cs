using System.Reflection;

namespace Api.Extensions;

public static class ModuleExtension
{

    public static Dictionary<Type,Assembly> GetModuleAssemblyTypes()
    {
        Dictionary<Type, Assembly> result = new Dictionary<Type, Assembly>();
        result.Add(typeof(SchoolIdentityDbContext),Identity.Application.AssemblyReference.Assembly);
        result.Add(typeof(schoolNotificationDbContext),Notification.AssemblyReference.Assembly);
        
        return result;
    }
    
}