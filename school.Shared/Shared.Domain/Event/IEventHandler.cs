namespace Shared.Domain.Event;

public interface IEventHandler<T> where T : IEvent
{
    public Task Handle(T domainEvent, CancellationToken cancellationToken);

    
}