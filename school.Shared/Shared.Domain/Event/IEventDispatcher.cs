namespace Shared.Domain.Event;

public interface IEventDispatcher
{
    public Task DispatchAsync(IEvent events, CancellationToken cancellationToken = default);

}