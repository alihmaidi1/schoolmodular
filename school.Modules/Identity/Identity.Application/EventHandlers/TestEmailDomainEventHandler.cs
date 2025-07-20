using Identity.Domain.Event;
using Shared.Domain.Event;

namespace Identity.Application.EventHandlers;

internal sealed class TestEmailDomainEventHandler:IEventHandler<SendEmailDomainEvent>
{
    public async Task Handle(SendEmailDomainEvent domainDomainEvent, CancellationToken cancellationToken)
    {
        
        Console.WriteLine("TestEmailEventHandler1");
    }
}