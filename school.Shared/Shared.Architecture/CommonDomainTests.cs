using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using Shared.Domain.Entities;
using Shared.Domain.Event;

namespace Shared.Architecture;

public abstract class CommonDomainTests
{
    
    
    protected static void AssertDomainEventShouldBeSealed(Assembly domainAssembly)
    {
        Types.InAssembly(domainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .BeSealed()
            .GetResult()
            .ShouldBeSuccessful();
    }
    
    
    protected static void AssertDomainEventShouldHaveDomainEventPostfix(Assembly domainAssembly)
    {
        Types.InAssembly(domainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .HaveNameEndingWith("DomainEvent")
            .GetResult()
            .ShouldBeSuccessful();
    }
    
    
    
    protected static void AssertEntitiesShouldHavePrivateParameterlessConstructor(Assembly domainAssembly)
    {
        var entityTypes = Types.InAssembly(domainAssembly)
            .That()
            .Inherit(typeof(Entity))
            .GetTypes();

        var failingTypes = new List<Type>();
        foreach (var entityType in entityTypes)
        {
            var constructors = entityType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            if (!Enumerable.Any(constructors, constructor => constructor.IsPrivate && constructor.GetParameters().Length == 0))
                failingTypes.Add(entityType);
        }

        failingTypes.Should().BeEmpty();
    }
    
    
    protected static void AssertEntitiesShouldOnlyHavePrivateConstructors(Assembly domainAssembly)
    {
        var entityTypes = Types.InAssembly(domainAssembly)
            .That()
            .Inherit(typeof(Entity))
            .GetTypes();

        var failingTypes = new List<Type>();
        foreach (var entityType in entityTypes)
        {
            var constructors = entityType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            if (!Enumerable.All(constructors, constructor => constructor.IsPrivate))
                failingTypes.Add(entityType);
        }

        failingTypes.Should().BeEmpty();
    }

}