using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Test.Modular;

public class ModularArchitectureTests
{
    
    [Fact]
    public void CommonModule_Should_Not_DependOnOtherModules()
    {        
            string[] otherModules = [
                "Notification",
                "Identity.Domain",
                "Identity.infrastructure",
                "Identity.Application"
                
            ];
            var result = Types
            .InAssembly(Common.AssemblyReference.Assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherModules)
            .GetResult();

            
            result.IsSuccessful.Should().BeTrue();

    }

    
    [Fact]
    public void NotificationModule_Should_Not_DependOnOtherModules()
    {        
        string[] otherModules = [
            "Common",
            "Identity.Domain",
            "Identity.infrastructure",
            "Identity.Application"
                
        ];
        var result = Types
            .InAssembly(Notification.AssemblyReference.Assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherModules)
            .GetResult();

            
        result.IsSuccessful.Should().BeTrue();

    }

    [Fact]
    public void IdentityModule_Should_Not_DependOnOtherModules()
    {        
        string[] otherModules = [
            "Common",
            "Notification",
                
        ];
        
        List<Assembly> IdentitiyAssemblies =
        [
            Identity.Domain.AssemblyReference.Assembly,
            Identity.infrastructure.AssemblyReference.Assembly,
            Identity.Application.AssemblyReference.Assembly,
        ];

        var result = Types
            .InAssemblies(IdentitiyAssemblies)
            .ShouldNot()
            .HaveDependencyOnAny(otherModules)
            .GetResult();

            
        result.IsSuccessful.Should().BeTrue();

    }

}