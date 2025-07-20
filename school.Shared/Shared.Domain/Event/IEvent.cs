namespace Shared.Domain.Event;

public interface IEvent
{
    public Guid EventId { get; set; }
    public DateTime OccurredOn { get; set; }
}