using Shared.Domain.Event;

namespace Identity.Domain.Event;

public sealed class SendEmailDomainEvent: Shared.Domain.Event.Event,IDomainEvent
{
    public string Email;
    public string Message;

    public SendEmailDomainEvent(string email, string message)
    {
        Email = email;
        
        EventId=Guid.NewGuid();
        Message = message;
        
    }

}