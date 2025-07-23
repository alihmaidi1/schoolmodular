using Microsoft.AspNetCore.Http;

namespace Shared.Domain.CQRS;

public interface IRequestHandler<TCommand, TResult> where TCommand : IRequest<TResult>
{
    public Task<TResult> Handle(TCommand request, CancellationToken cancellationToken);
  
    
}

public interface IRequestHandler<TCommand> where TCommand : IRequest
{
    public Task Handle(TCommand request, CancellationToken cancellationToken);
    
}