using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Shared.Application.CQRS;
using Shared.Domain.CQRS;
using Shared.Domain.OperationResult;

namespace Shared.Application.PiplineBehavior;

[PiplineOrder(2)]
public class IdempotencyBehavior<TRequest,TResponse>: IPipelineBehavior<TRequest,TResponse>
    where TRequest : IRequest<TResponse> where TResponse: IResult
{
    
    private readonly ILogger<IdempotencyBehavior<TRequest,TResponse>> logger;
    private readonly IMemoryCache _cache;

    public IdempotencyBehavior(IMemoryCache cache,ILogger<IdempotencyBehavior<TRequest,TResponse>> _logger)
    {
        logger = _logger;
        _cache = cache;
        
    }
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, Func<Task<TResponse>> next)
    {
        if (request.RequestId==null)
         {
             logger.LogWarning("Idempotency check failed: RequestId is null for {CommandType}", typeof(TRequest).Name);
             return (TResponse)Result.ValidationFailure<object>(Error.ValidationFailures("Idempotency check failed: RequestId is null")).ToActionResult();
         }
         string cacheKey = $"Idempotent_{typeof(TRequest).Name}_{request.RequestId}";
         if (_cache.TryGetValue(cacheKey, out var cachedResult))
         {
             logger.LogInformation("Returning cached idempotent result for {CacheKey}", cacheKey);
             return (TResponse)Result.Conflict<object>(Error.AlreadyProcessed).ToActionResult();
              
         }
             
         var result = await next();
         _cache.Set(cacheKey, cacheKey,TimeSpan.FromSeconds(60));
         return result;

    }
}