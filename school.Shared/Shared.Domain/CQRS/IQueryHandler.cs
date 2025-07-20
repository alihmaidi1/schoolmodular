namespace Shared.Domain.CQRS;

public interface IQueryHandler<TCommand,TResult>:IRequestHandler<TCommand,TResult> where TCommand : IQuery<TResult>
{
    public Task<TResult> Handle(TCommand request, CancellationToken cancellationToken);
    
}