using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Application.CQRS;
using Shared.Domain.CQRS;
using Shared.Domain.OperationResult;

namespace Shared.Application.PiplineBehavior;

[PiplineOrder(1)]
public class LoggingBehavior<TRequest,TResponse>: IPipelineBehavior<TRequest,TResponse>
    where TRequest : IRequest<TResponse> where TResponse : Result
{

    private readonly ILogger<LoggingBehavior<TRequest,TResponse>> logger; 
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest,TResponse>> _logger)
    {
     
        logger = _logger;
        
    }
    
    
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, Func<Task<TResponse>> next)
    {
        logger.LogInformation("[START] Handle request={Request} - RequestData={RequestData}",
             typeof(TRequest).Name, request);
         var timer = new Stopwatch();
         timer.Start();
         var response=await next();
         timer.Stop();
         var timeTaken = timer.Elapsed;
         if (timeTaken.Seconds > 3) // if the request is greater than 3 seconds, then log the warnings
             logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken} seconds.",
                 typeof(TRequest).Name, timeTaken.Seconds);

         logger.LogInformation("[END] Handled {Request}", typeof(TRequest).Name);
         return response;

    }
}