using Identity.Domain.Event;
using Shared.Domain.CQRS;
using Shared.Domain.Event;

namespace Identity.Application.EventHandlers;

internal sealed class SendEmailDomainEventHandler: IEventHandler<SendEmailDomainEvent>
{
    public async Task Handle(SendEmailDomainEvent domainDomainEvent, CancellationToken cancellationToken)
    {
        Console.WriteLine("TestEmailEventHandler2");

    }
}