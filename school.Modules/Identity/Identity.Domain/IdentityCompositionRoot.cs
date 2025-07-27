using Autofac;

namespace Identity.Domain;

public static class IdentityCompositionRoot
{
    private static IContainer _container;
    public static void SetContainer(IContainer container)
    {
        _container = container;
    }

    public static ILifetimeScope BeginLifetimeScope()
    {
        return _container.BeginLifetimeScope();
    }
    
    
}