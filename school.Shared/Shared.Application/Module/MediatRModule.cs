using System.Reflection;
using Autofac;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Application.PiplineBehavior;

namespace Shared.Application.Module;

public class MediatRModule: Autofac.Module
{
    private readonly Assembly _applicationAssembly;

    public MediatRModule(Assembly assembly)
    {

        _applicationAssembly = assembly;
    }
        
    protected override void Load(ContainerBuilder builder)
    {
        
        
        builder.RegisterType<ServiceProviderWrapper>()
            .As<IServiceProvider>()
            .InstancePerDependency();


        
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        
        
        builder.RegisterAssemblyTypes(_applicationAssembly)
            .AsClosedTypesOf(typeof(IRequestHandler<,>))  // MediatR Handlers
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        builder.RegisterGeneric(typeof(Logger<>))
            .As(typeof(ILogger<>))
            .SingleInstance();
        builder.RegisterGeneric(typeof(LoggingBehavior<,>))
            .As(typeof(IPipelineBehavior<,>))
            .InstancePerLifetimeScope();
        
        
        builder.RegisterGeneric(typeof(ValidationBehavior<,>))
            .As(typeof(IPipelineBehavior<,>))
            .InstancePerLifetimeScope();
        
        builder.RegisterAssemblyTypes(_applicationAssembly)
            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        

    }
}