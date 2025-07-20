using Shared.Domain.CQRS;
using Shared.Domain.Event;

namespace Shared.Domain.Entities;

public class Aggregate: Entity,IAggregate
{

    private List<IDomainEvent> _domainEvents { get; } = new List<IDomainEvent>();
    public List<IDomainEvent> GetDomainEvents()=> _domainEvents.ToList();

    public IDomainEvent[] ClearDomainEvents()
    {
        
        IDomainEvent[] dequeuedEvents = _domainEvents.ToArray();
        _domainEvents.Clear();
        return dequeuedEvents;
    }

    public void RaiseDomainEvent(IDomainEvent @event)
    {
        _domainEvents.Add(@event);
        
        
    }
}