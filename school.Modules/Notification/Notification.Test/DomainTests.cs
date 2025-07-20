using System.Reflection;
using Shared.Architecture;

namespace Notification.Test;

public class DomainTests: CommonDomainTests
{
    private static readonly Assembly DomainAssembly = Notification.AssemblyReference.Assembly;
    
        
    [Fact]
    public void DomainEvents_Should_BeSealed()
    {
        AssertDomainEventShouldBeSealed(DomainAssembly);
    }
    
    
    [Fact]
    public void DomainEvent_ShouldHave_DomainEventPostfix()
    {
        AssertDomainEventShouldHaveDomainEventPostfix(DomainAssembly);
    }
    
    
    [Fact]
    public void Entities_ShouldHave_PrivateParameterlessConstructor()
    {
        AssertEntitiesShouldHavePrivateParameterlessConstructor(DomainAssembly);
    }
    
    
    [Fact]
    public void Entities_ShouldOnlyHave_PrivateConstructors()
    {
        AssertEntitiesShouldOnlyHavePrivateConstructors(DomainAssembly);
    }
    
    
    
}