using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Shared.Domain.OperationResult;

namespace Shared.Application.PiplineBehavior;

public class ValidationBehavior<TRequest,TResponse>: IPipelineBehavior<TRequest,TResponse>
    where TRequest : IRequest<TResponse> where TResponse: Result

{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
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

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        
        var validationFailures = await ValidateAsync(request, _validators);
        if (validationFailures == null)
        {
            return await next();
        }

        throw new ValidationException(validationFailures);
    }
}