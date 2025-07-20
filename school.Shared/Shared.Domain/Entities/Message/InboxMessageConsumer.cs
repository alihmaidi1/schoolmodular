namespace Shared.Domain.Entities.Message;

public class InboxMessageConsumer: MessageConsumer,IMessageConsumer
{


    public InboxMessageConsumer(Guid messageId, string name): base(messageId,name)
    {
        
        
        
    }
}