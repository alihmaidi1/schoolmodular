using MediatR;

namespace Shared.Domain.MediatR;

public interface ICommand<TResult>: IRequest<TResult>
{
    
    
    
}

public interface ICommand : IRequest
{
    
    
}