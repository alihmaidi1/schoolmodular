namespace Shared.Domain.CQRS;

public interface IPipelineBehavior<in TRequest, TResult> 
    where TRequest : IRequest<TResult>
{
    
    Task<TResult> Handle(TRequest request, 
        CancellationToken cancellationToken, 
        Func<Task<TResult>> next);
}


public interface IPipelineBehavior<in TRequest> 
    where TRequest : IRequest
{
    
    Task Handle(TRequest request, 
        CancellationToken cancellationToken, 
        Func<Task> next);
}