using Autofac;
using Identity.Domain;
using Identity.infrastructure;
using MediatR;
using Shared.Domain.MediatR;

namespace Identity.Presentation;

internal static class CommandsExecutor
{
    
    internal static async Task Execute(ICommand command)
    {
        using (var scope = IdentityCompositionRoot.BeginLifetimeScope())
        {
            var mediator = scope.Resolve<IMediator>();
            await mediator.Send(command);
        }
    }

    internal static async Task<TResult> Execute<TResult>(ICommand<TResult> command)
    {
        using (var scope = IdentityCompositionRoot.BeginLifetimeScope())
        {
            var mediator = scope.Resolve<IMediator>();
            return await mediator.Send(command);
        }
    }
    
}