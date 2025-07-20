namespace Shared.Domain.Event;

public abstract class Event: IEvent
{
    
    public Guid EventId { get; set; }
    public DateTime OccurredOn { get; set; }
}