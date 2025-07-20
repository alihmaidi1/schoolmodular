namespace Shared.Domain.CQRS;

public interface ICommandHandler<TCommand,TResult>:IRequestHandler<TCommand,TResult> where TCommand : ICommand<TResult>
{
    
    public Task<TResult> Handle(TCommand request, CancellationToken cancellationToken);
    


}