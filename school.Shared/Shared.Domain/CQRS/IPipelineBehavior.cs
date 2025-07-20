namespace Shared.Domain.CQRS;

public interface IPipelineBehavior<in TRequest, TResult> 
    where TRequest : IRequest<TResult>
{
    
    Task<TResult> Handle(TRequest request, 
        CancellationToken cancellationToken, 
        Func<Task<TResult>> next);
}