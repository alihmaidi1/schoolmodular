using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Event;

namespace Shared.Application.CQRS;

public class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{
    private static readonly ConcurrentDictionary<Type, Type> _handlerTypeCache = new();
    private static readonly ConcurrentDictionary<Type, MethodInfo> _handleMethodCache = new();

    public async Task DispatchAsync(IEvent @event, CancellationToken cancellationToken = default) 
    {
        await DispatchEventAsync(@event, cancellationToken);
        
    }

    private async Task DispatchEventAsync(IEvent @event, CancellationToken cancellationToken) 
    {
        using var scope = serviceProvider.CreateAsyncScope();
        var eventType = @event.GetType();
        
        // الحصول على نوع الـ handler مع التخزين المؤقت
        var handlerType = _handlerTypeCache.GetOrAdd(
            eventType,
            t => typeof(IEventHandler<>).MakeGenericType(t));

        var handlers = scope.ServiceProvider.GetServices(handlerType);
        foreach (var handler in handlers)
        {
            if (handler is null)
            {
                continue;
            }
            await HandleSingleEventAsync(handler, @event, cancellationToken);
            
        }
    }

    private async Task HandleSingleEventAsync(object handler, IEvent @event, CancellationToken cancellationToken)
    {
        try
        {
            var handleMethod = handler.GetType().GetMethod("Handle") ??
                               throw new InvalidOperationException("Handle method not found");

            await (Task)handleMethod.Invoke(handler, new object[] { @event, cancellationToken })!;
        }
        catch (Exception ex)
        {
            // يمكنك إضافة logging هنا
            throw new InvalidOperationException($"Error handling event {@event.GetType().Name}", ex);
        }
    }
}