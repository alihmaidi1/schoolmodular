using MediatR;

namespace Shared.Domain.MediatR;

public interface ICommandHandler<TCommand,TResult>:IRequestHandler<TCommand,TResult> where TCommand : ICommand<TResult>
{
}

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand> where TCommand : IRequest
{
    
    
    
} 