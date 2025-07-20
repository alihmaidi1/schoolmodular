namespace Shared.Domain.CQRS;

public interface IRequest<TResult>
{
 
    public Guid? RequestId { get; set; }   
}