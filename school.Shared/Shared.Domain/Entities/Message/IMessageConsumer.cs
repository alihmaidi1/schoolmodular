namespace Shared.Domain.Entities.Message;

public interface IMessageConsumer
{
    
    public Guid MessageId { get; init; }

    public string Name { get; init; }
}