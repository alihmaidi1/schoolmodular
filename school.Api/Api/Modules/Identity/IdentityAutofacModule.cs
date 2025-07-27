using Autofac;
using Identity.Domain;
using Identity.Presentation;

namespace Api.Modules.Identity;

public class IdentityAutofacModule: Module
{
    
    
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<IdentityModule>()
            .As<IIdentityModule>()
            .InstancePerLifetimeScope();
        
        
    }
    
}