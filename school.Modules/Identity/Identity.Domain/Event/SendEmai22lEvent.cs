using Shared.Domain.Event;

namespace Identity.Domain.Event;

public class SendEmai22lEvent: Shared.Domain.Event.Event,IIntegrationEvent
{
    
    public string Email;
    public string Message;

    public SendEmai22lEvent(string email, string message)
    {
        Email = email;
        
        EventId=Guid.NewGuid();
        Message = message;
        
    }
    
}