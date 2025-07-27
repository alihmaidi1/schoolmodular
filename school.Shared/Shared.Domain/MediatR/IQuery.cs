using MediatR;

namespace Shared.Domain.MediatR;

public interface IQuery<TResult>: IRequest<TResult>
{
    
}