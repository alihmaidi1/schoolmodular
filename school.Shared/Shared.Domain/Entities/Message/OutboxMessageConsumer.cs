namespace Shared.Domain.Entities.Message;

public class OutboxMessageConsumer: MessageConsumer,IMessageConsumer
{


    public OutboxMessageConsumer(Guid messageId,string name): base(messageId,name)
    {
        
    }

}