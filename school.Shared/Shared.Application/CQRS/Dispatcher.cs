using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.CQRS;

namespace Shared.Application.CQRS;

public class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private static readonly ConcurrentDictionary<Type, Type> _voidCommandHandlerTypes = new();
    private static readonly ConcurrentDictionary<(Type requestType, Type resultType), Type> _handlerTypeCache = new();
    private static readonly ConcurrentDictionary<Type, MethodInfo> _handlerMethodCache = new();

    public Dispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> Send<TResult>(IRequest<TResult> request, CancellationToken cancellationToken = default)
    {
        var requestType = request.GetType();
        var cacheKey = (requestType, typeof(TResult));

        var handlerType = _handlerTypeCache.GetOrAdd(cacheKey,
            _ => typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResult)));

        return await InvokePipelineWithResult(request, handlerType, cancellationToken);
    }


    public async Task Send(IRequest request, CancellationToken cancellationToken = default)
    {
        var requestType = request.GetType();

        var handlerType = _voidCommandHandlerTypes.GetOrAdd(requestType,
            t => typeof(IRequestHandler<>).MakeGenericType(t));

        await InvokeVoidPipeline(request, handlerType, cancellationToken);
    }
    
    
    
    private async Task<TResult> InvokePipelineWithResult<TResult>(
        IRequest<TResult> request, 
        Type handlerType, 
        CancellationToken cancellationToken)
    {
        var requestType = request.GetType();
        var behaviorType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, typeof(TResult));
    
        var behaviors = GetOrderedBehaviors(behaviorType);
        Func<Task<TResult>> handler = () => InvokeHandler<TResult>(handlerType, request, cancellationToken);
    
        foreach (var behavior in behaviors)
        {
            var currentHandler = handler;
            handler = () => (Task<TResult>)behavior.GetType().GetMethod("Handle")!.Invoke(
                behavior,
                new object[] { request, cancellationToken, currentHandler })!;
        }
    
        return await handler();
    }
    
    private async Task InvokeVoidPipeline(
        IRequest request, 
        Type handlerType, 
        CancellationToken cancellationToken)
    {
        var requestType = request.GetType();
        var behaviorType = typeof(IPipelineBehavior<>).MakeGenericType(requestType);
    
        var behaviors = GetOrderedBehaviors(behaviorType);
        Func<Task> handler = () => InvokeVoidHandler(handlerType, request, cancellationToken);
    
        foreach (var behavior in behaviors)
        {
            var currentHandler = handler;
            handler = () => (Task)behavior.GetType().GetMethod("Handle")!.Invoke(
                behavior,
                new object[] { request, cancellationToken, currentHandler })!;
        }
    
        await handler();
    }
    
    private IEnumerable<object> GetOrderedBehaviors(Type behaviorType)
    {
        return (IEnumerable<object>)_serviceProvider.GetServices(behaviorType)
            .OrderBy(x => x.GetType().GetCustomAttribute<PiplineOrderAttribute>()?.Order ?? 0)
            .Reverse()
            .ToList();
    }
    
    
    private async Task<TResult> InvokeHandler<TResult>(Type handlerType, object request, CancellationToken cancellationToken)
    {
        var handler = _serviceProvider.GetRequiredService(handlerType);
        var method = _handlerMethodCache.GetOrAdd(handlerType,
            t => t.GetMethod("Handle") ?? throw new InvalidOperationException($"Handle method not found on {t.Name}"));

        var result = method.Invoke(handler, new[] { request, cancellationToken });
        return await (Task<TResult>)result!;
    }

    private async Task InvokeVoidHandler(Type handlerType, object request, CancellationToken cancellationToken)
    {
        var handler = _serviceProvider.GetRequiredService(handlerType);
        var method = _handlerMethodCache.GetOrAdd(handlerType,
            t => t.GetMethod("Handle") ?? throw new InvalidOperationException($"Handle method not found on {t.Name}"));

        var result = method.Invoke(handler, new[] { request, cancellationToken });
        await (Task)result!;
    }

}