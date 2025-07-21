using System.Transactions;
using Shared.Application.CQRS;
using Shared.Domain.CQRS;
using Shared.Domain.OperationResult;

namespace Shared.Application.PiplineBehavior;


[PiplineOrder(2)]

public class UnitOfWorkBehavior<TRequest,TResponse>: IPipelineBehavior<TRequest,TResponse>
    where TRequest : IRequest<TResponse> where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, Func<Task<TResponse>> next)
    {
        using var transactionScope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);
        var result =await next();
        if (result.IsSuccess)
        {
            transactionScope.Complete();
        }
        return result;

    }
}