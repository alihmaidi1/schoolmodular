
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Exception;
using Shared.Domain.OperationResult;

namespace Api.Middleware;

public class GlobalExceptionHandlingMiddleware: IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);

        }
        
        catch (ValidationException e)
        {
            var response = Result.ValidationFailure<object>(Error.ValidationFailures(e.Message));
            context.Response.StatusCode = (int)response.statusCode;
            await context.Response.WriteAsJsonAsync(response);

        }
        catch (IdempotencyException e)
        {

            ProblemDetails problem = new ProblemDetails()
            {
                Status = StatusCodes.Status409Conflict,
                Type = "IdempotencyException",
                Detail = e.Message,
                Title = "IdempotencyException"
                
            };
            var json = JsonSerializer.Serialize(problem);
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);

            
        }
        catch (Exception e)
        {

            ProblemDetails problem = new ProblemDetails()
            {
                
                Status = StatusCodes.Status500InternalServerError,
                Type = "Server Error",
                Detail = e.Message,
                Title = "Server Error"
                
            };
            var json = JsonSerializer.Serialize(problem);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }

    }
    
}