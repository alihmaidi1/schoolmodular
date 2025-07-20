using Polly;
using Polly.Retry;
using Refit;

namespace Shared.Infrastructure.Extensions;

public static class PollyExtensions
{
    
    public static IAsyncPolicy<HttpResponseMessage> GetTimeOutPolicy(int seconds)=> Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(seconds));

    public static  AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy() =>
        Policy<HttpResponseMessage>
            .Handle<ApiException>(ex => ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(3, retryAttempt =>
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

}