namespace Shared.Domain.CQRS;

public interface IDispatcher
{
    
    Task<TResult> Send<TResult>(IRequest<TResult> command, CancellationToken cancellationToken = default);
    Task Send(IRequest command, CancellationToken cancellationToken = default);

}