using System.Diagnostics;
using MediatR;
using Serilog;

namespace Shared.Application.PiplineBehavior;

public class LoggingBehavior<TRequest,TResponse>: IPipelineBehavior<TRequest,TResponse>
    where TRequest : IRequest<TResponse> 
{

    private readonly ILogger logger; 
    public LoggingBehavior(ILogger _logger)
    {
     
        logger = _logger;
        
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.Information("[START] Handle request={Request} - RequestData={RequestData}",
            typeof(TRequest).Name, request);
        var timer = new Stopwatch();
        timer.Start();
        var response=await next();
        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3) // if the request is greater than 3 seconds, then log the warnings
            logger.Warning("[PERFORMANCE] The request {Request} took {TimeTaken} seconds.",
                typeof(TRequest).Name, timeTaken.Seconds);

        logger.Information("[END] Handled {Request}", typeof(TRequest).Name);
        return response;

    }
}