using System.Transactions;
using MediatR;
using Shared.Domain.OperationResult;

namespace Shared.Application.PiplineBehavior;



public class UnitOfWorkBehavior<TRequest,TResponse>: IPipelineBehavior<TRequest,TResponse>
    where TRequest : IRequest<TResponse> where TResponse : Result
{

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using var transactionScope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);
        var result =await next();
        if (result.isSuccess)
        {
            transactionScope.Complete();
        }
        return result;

    }
}