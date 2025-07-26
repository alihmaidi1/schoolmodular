using Autofac;
using FluentValidation;
using MediatR;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Shared.Application.PiplineBehavior;
using Shared.Infrastructure.Modules;

namespace Identity.Presentation;

public class MediatRModule: Autofac.Module
{

    protected override void Load(ContainerBuilder builder)
    {
        var applicationAssembly = Identity.Application.AssemblyReference.Assembly;
        
        
        builder.RegisterType<ServiceProviderWrapper>()
            .As<IServiceProvider>()
            .InstancePerDependency();


        
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        
        
        builder.RegisterAssemblyTypes(applicationAssembly)
            .AsClosedTypesOf(typeof(IRequestHandler<,>))  // MediatR Handlers
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        builder.RegisterGeneric(typeof(Logger<>))
            .As(typeof(ILogger<>))
            .SingleInstance();
        // builder.RegisterGeneric(typeof(LoggingBehavior<,>))
        //     .As(typeof(IPipelineBehavior<,>))
        //     .InstancePerLifetimeScope();
        
        // builder.RegisterGeneric(typeof(UnitOfWorkBehavior<,>))
        //     .As(typeof(IPipelineBehavior<,>))
        //     .InstancePerLifetimeScope();
        
        // builder.RegisterGeneric(typeof(ValidationBehavior<,>))
        //     .As(typeof(IPipelineBehavior<,>))
        //     .InstancePerLifetimeScope();
        
        // builder.RegisterAssemblyTypes(applicationAssembly)
        //     .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
        //     .AsImplementedInterfaces()
        //     .InstancePerLifetimeScope();
        

    }
}