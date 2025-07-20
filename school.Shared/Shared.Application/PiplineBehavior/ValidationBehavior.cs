using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.AspNetCore.Http;
using Shared.Application.CQRS;
using Shared.Domain.CQRS;
using Shared.Domain.OperationResult;

namespace Shared.Application.PiplineBehavior;

[PiplineOrder(3)]
public class ValidationBehavior<TRequest,TResponse>: IPipelineBehavior<TRequest,TResponse>
    where TRequest : IRequest<TResponse> where TResponse: IResult

{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, Func<Task<TResponse>> next)
    {
        var validationFailures = await ValidateAsync(request, _validators);
         if (validationFailures == null)
         {
             return await next();
         }
         var result = Result.ValidationFailure<object>(Error.ValidationFailures(validationFailures)).ToActionResult();
         return (TResponse)result;
    }
    
    
    private static async Task<string?> ValidateAsync<TCommand>(TCommand request, IEnumerable<IValidator<TCommand>> validators)
     {
         var enumerable = validators.ToList();
         if (!enumerable.Any())
         {

             return null;
         }
         var context = new ValidationContext<TCommand>(request);

         ValidationResult[] validationResult = await Task.WhenAll(enumerable.Select(validator => validator.ValidateAsync(context)));
         var validationFailures = validationResult
         .Where(validationResult => !validationResult.IsValid)
         .SelectMany(validationResult => validationResult.Errors)
         .ToList();

         if (!validationFailures.Any())
         {

             return null;
         }
         else
         {

             return validationFailures.First().ErrorMessage;
         }

     }

}