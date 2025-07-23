namespace Shared.Domain.CQRS;

public interface ICommand<TResult>: IRequest<TResult>
{
    
    
    
}

public interface ICommand : IRequest
{
    
    
}