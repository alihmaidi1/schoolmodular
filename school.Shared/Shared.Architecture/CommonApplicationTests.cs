using System.Reflection;
using FluentValidation;
using NetArchTest.Rules;
using Shared.Domain.CQRS;
using Shared.Domain.Event;

namespace Shared.Architecture;

public abstract class CommonApplicationTests
{
    
    protected static void AssertCommandsShouldBeSealed(Assembly applicationAssembly)
    {
        Types.InAssembly(applicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommand<>))
            .Should()
            .BeSealed()
            .GetResult()
            .ShouldBeSuccessful();
        
    }
    
    
    protected static void AssertCommandsShouldHaveNameEndingWithCommand(Assembly applicationAssembly)
    {
        Types.InAssembly(applicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommand<>))
            .Should()
            .HaveNameEndingWith("Command")
            .GetResult()
            .ShouldBeSuccessful();
    }
    
    protected static void AssertCommandHandlersShouldNotBePublic(Assembly applicationAssembly)
    {
        Types.InAssembly(applicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .NotBePublic()
            .GetResult()
            .ShouldBeSuccessful();
    }
    
    
    protected static void AssertCommandHandlersShouldBeSealed(Assembly applicationAssembly)
    {
        Types.InAssembly(applicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .BeSealed()
            .GetResult()
            .ShouldBeSuccessful();
    }
    
    
    protected static void AssertCommandHandlersShouldHaveNameEndingWithCommandHandler(Assembly applicationAssembly)
    {
        Types.InAssembly(applicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .GetResult()
            .ShouldBeSuccessful();
    }
    
    
    protected static void AssertQueriesShouldBeSealed(Assembly applicationAssembly)
    {
        Types.InAssembly(applicationAssembly)
            .That()
            .ImplementInterface(typeof(IQuery<>))
            .Should()
            .BeSealed()
            .GetResult()
            .ShouldBeSuccessful();
    }
    
    
    protected static void AssertQueriesShouldHaveNameEndingWithQuery(Assembly applicationAssembly)
    {
        Types.InAssembly(applicationAssembly)
            .That()
            .ImplementInterface(typeof(IQuery<>))
            .Should()
            .HaveNameEndingWith("Query")
            .GetResult()
            .ShouldBeSuccessful();
    }
    
    
    protected static void AssertQueryHandlersShouldNotBePublic(Assembly applicationAssembly)
    {
        Types.InAssembly(applicationAssembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .NotBePublic()
            .GetResult()
            .ShouldBeSuccessful();
    }
    
    
    protected static void AssertQueryHandlersShouldBeSealed(Assembly applicationAssembly)
    {
        Types.InAssembly(applicationAssembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .BeSealed()
            .GetResult()
            .ShouldBeSuccessful();
    }
    
    
    protected static void AssertQueryHandlersShouldHaveNameEndingWithQueryHandler(Assembly applicationAssembly)
    {
        Types.InAssembly(applicationAssembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .HaveNameEndingWith("QueryHandler")
            .GetResult()
            .ShouldBeSuccessful();
    }
    
    
    protected static void AssertValidatorsShouldNotBePublic(Assembly applicationAssembly)
    {
        Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .NotBePublic()
            .GetResult()
            .ShouldBeSuccessful();
    }
    
    
    protected static void AssertValidatorsShouldBeSealed(Assembly applicationAssembly)
    {
        Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .BeSealed()
            .GetResult()
            .ShouldBeSuccessful();
    }
    
    
    
    protected static void AssertValidatorsShouldHaveNameEndingWithValidation(Assembly applicationAssembly)
    {
        Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .HaveNameEndingWith("Validation")
            .GetResult()
            .ShouldBeSuccessful();
    }
    
    
    protected static void AssertDomainEventHandlersShouldNotBePublic(Assembly applicationAssembly)
    {
        Types.InAssembly(applicationAssembly)
            .That()
            .ImplementInterface(typeof(IEventHandler<>))
            .Should()
            .NotBePublic()
            .GetResult()
            .ShouldBeSuccessful();
    }
    
    
    protected static void AssertDomainEventHandlersShouldBeSealed(Assembly applicationAssembly)
    {
        Types.InAssembly(applicationAssembly)
            .That()
            .ImplementInterface(typeof(IEventHandler<>))
            .Should()
            .BeSealed()
            .GetResult()
            .ShouldBeSuccessful();
    }
    
    
    protected static void AssertDomainEventHandlersShouldHavseNameEndingWithDomainEventHandler(Assembly applicationAssembly)
    {
        Types.InAssembly(applicationAssembly)
            .That()
            .ImplementInterface(typeof(IEventHandler<>))
            .Should()
            .HaveNameEndingWith("DomainEventHandler")
            .GetResult()
            .ShouldBeSuccessful();
    }

}