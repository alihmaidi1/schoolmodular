using System.Net;
using Microsoft.AspNetCore.Http;

namespace Shared.Domain.OperationResult;

public class Result
{
    protected Result(bool isSuccess, HttpStatusCode statusCode, Error? error = null)
    {
        if (isSuccess && error != null)
        {
            throw new InvalidOperationException("Successful results cannot contain errors");
        }

        if (!isSuccess && error == null)
        {
            throw new InvalidOperationException("Failed results must contain an error");
        }

        IsSuccess = isSuccess;
        Error = error;
        StatusCode = statusCode;
    }

    public bool IsSuccess { get; }
    public HttpStatusCode StatusCode { get; }
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; }

    // Success cases
    public static TResult<TValue> Success<TValue>(TValue value) => 
        new(value, true, HttpStatusCode.OK);
    
    public static TResult<TValue> Success<TValue>() where TValue : new() => 
        new(new TValue(), true, HttpStatusCode.OK);

    // Failure cases
    public static TResult<TValue> Failure<TValue>(Error error, HttpStatusCode statusCode) => 
        new(default, false, statusCode, error);
    
    public static TResult<TValue> NotFound<TValue>(Error error) => 
        new(default, false, HttpStatusCode.NotFound, error);
    
    public static TResult<TValue> ValidationFailure<TValue>(Error error) => 
        new(default, false, HttpStatusCode.UnprocessableContent, error);
    
    public static TResult<TValue> InternalError<TValue>(Error error) => 
        new(default, false, HttpStatusCode.InternalServerError, error);
    
    public static TResult<TValue> Conflict<TValue>(Error error) => 
        new(default, false, HttpStatusCode.Conflict, error);

    // Factory method
    public static TResult<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : InternalError<TValue>(Error.NullValue);
}

// Extension methods for IResult conversion
public static class ResultExtensions
{
    public static IResult ToActionResult<TValue>(this TResult<TValue> result)
    {
        return result.IsSuccess
            ? Results.Ok(result)
            : result.StatusCode switch
            {
                HttpStatusCode.NotFound => Results.NotFound(result),
                HttpStatusCode.UnprocessableContent => Results.UnprocessableEntity(result),
                HttpStatusCode.Conflict => Results.Conflict(result),
                _ => Results.StatusCode((int)result.StatusCode)
            };
    }
    public static IResult ToActionResult(this Result result)
    {
        return result.IsSuccess
            ? Results.Ok(result)
            : result.StatusCode switch
            {
                HttpStatusCode.NotFound => Results.NotFound(result),
                HttpStatusCode.UnprocessableContent => Results.UnprocessableEntity(result),
                HttpStatusCode.Conflict => Results.Conflict(result),
                _ => Results.StatusCode((int)result.StatusCode)
            };
    }
}