using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Entities.Message;
using Shared.Domain.Event;

namespace Shared.Application.Message;

public static class MessageExtensions
{
    public static IServiceCollection  AddEventHandlersWithDecorate<TMessageConsume,TEvent>(this IServiceCollection services, Assembly assembly, Type dbContextType)
    where TMessageConsume: class, IMessageConsumer where TEvent: IEvent
    {

        var eventTypes = assembly.GetTypes()
            .SelectMany(t => t.GetInterfaces())
            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>))
            .Select(i => i.GetGenericArguments()[0])
            .Where(x=>x.IsAssignableTo(typeof(TEvent)))
            .Distinct()
            .ToList();
        foreach (var eventType in eventTypes)
        {
            var handlerInterface = typeof(IEventHandler<>).MakeGenericType(eventType);
            
            var handlers = assembly.GetTypes()
                .Where(t => t.IsSealed && !t.IsAbstract)
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i == handlerInterface))
                .ToList();

            foreach (var handlerType in handlers)
            {
                services.AddScoped(handlerInterface, handlerType);
            }

            var decoratorType = typeof(IdempotentEventHandler<,,>)
                .MakeGenericType(dbContextType, typeof(TMessageConsume), eventType);
            services.TryDecorate(handlerInterface, (inner, sp) =>
            {
                return ActivatorUtilities.CreateInstance(sp, decoratorType, inner);
            });
            
            
        }



        return services;
    }

    
    
}