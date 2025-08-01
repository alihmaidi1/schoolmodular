using Autofac;

namespace Shared.Application.Module;

public class ServiceProviderWrapper: IServiceProvider
{
    private readonly ILifetimeScope lifeTimeScope;

    
    public ServiceProviderWrapper(ILifetimeScope lifeTimeScope)
    {
        this.lifeTimeScope = lifeTimeScope;
    }
    
    #nullable enable
    public object? GetService(Type serviceType) => this.lifeTimeScope.Resolve(serviceType);
    
}