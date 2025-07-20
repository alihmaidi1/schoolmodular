using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Shared.Architecture;

namespace Identity.ArchitectureTest;

public class LayerTests: CommonLayerTests
{
    
    private static readonly Assembly ApplicationAssembly = Identity.Application.AssemblyReference.Assembly;
    private static readonly Assembly DomainAssembly = Identity.Domain.AssemblyReference.Assembly;
    private static readonly Assembly InfrastructureAssembly = Identity.infrastructure.AssemblyReference.Assembly;

    
    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_ApplicationLayer()
    {
        AssertLayerDoesNotHaveDependencyOnAnotherLayer(
            DomainAssembly, 
            ApplicationAssembly);
    }
    
    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        AssertLayerDoesNotHaveDependencyOnAnotherLayer(
            DomainAssembly, 
            InfrastructureAssembly);
    }
    
    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        AssertLayerDoesNotHaveDependencyOnAnotherLayer(
            ApplicationAssembly, 
            InfrastructureAssembly);
    }

    
}
