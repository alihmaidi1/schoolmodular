using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Infrastructure.Security;

public static class LimitRate
{
    public static IServiceCollection AddLimitRate(this IServiceCollection services)
    {

        services.AddRateLimiter(option =>
        {
            option.AddPolicy("fixed", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    httpContext.Connection.RemoteIpAddress?.ToString(),
                    _ => new FixedWindowRateLimiterOptions
                    {
                        Window = TimeSpan.FromSeconds(40),
                        PermitLimit = 3,
                        QueueLimit = 0,
                        QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst

                    }
                )
            );
            option.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

        });
        return services;
    }

    
}