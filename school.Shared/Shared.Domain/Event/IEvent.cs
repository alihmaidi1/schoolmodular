using MediatR;

namespace Shared.Domain.Event;

public interface IEvent:INotification
{
    public Guid EventId { get; set; }
    public DateTime OccurredOn { get; set; }
}