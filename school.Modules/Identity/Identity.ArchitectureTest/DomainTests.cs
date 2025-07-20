using System.Reflection;
using Identity.Domain.Security;
using Shared.Architecture;

namespace Identity.ArchitectureTest;

public class DomainTests: CommonDomainTests
{
    private static readonly Assembly DomainAssembly = Identity.Domain.AssemblyReference.Assembly;
    
        
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