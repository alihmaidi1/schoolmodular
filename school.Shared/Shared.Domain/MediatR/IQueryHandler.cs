using MediatR;

namespace Shared.Domain.MediatR;

public interface IQueryHandler<TCommand,TResult>:IRequestHandler<TCommand,TResult> where TCommand : IQuery<TResult>
{
    
}