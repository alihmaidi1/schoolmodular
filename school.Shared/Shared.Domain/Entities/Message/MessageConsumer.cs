namespace Shared.Domain.Entities.Message;

public class MessageConsumer: IMessageConsumer
{

    public MessageConsumer(Guid eventId, string name)
    {
        MessageId = eventId;
        Name = name;
    }
    
    public Guid MessageId { get; init; }

    public string Name { get; init; }
    
}