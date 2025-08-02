using System.Transactions;
using Shared.Domain.MediatR;
using Shared.Domain.OperationResult;
using Shared.Domain.Repositories;

namespace Shared.Application.Module;

public class UnitOfWorkCommandHandlerDecorator<T,TResult>: ICommandHandler<T,TResult> 
    where T : ICommand<TResult>
    where TResult : Result
{
    
    private IUnitOfWork  _unitOfWork;
    private readonly ICommandHandler<T, TResult> _decorated;
    


    public UnitOfWorkCommandHandlerDecorator(ICommandHandler<T, TResult> decorated, IUnitOfWork unitOfWork)
    {
        
        _decorated = decorated;
        _unitOfWork = unitOfWork;
        
    }
    
    public async Task<TResult> Handle(T request, CancellationToken cancellationToken)
    {
        using (var scope=new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
        
            var result= await _decorated.Handle(request, cancellationToken);
            if (result.isSuccess)
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                scope.Complete();
                                
            }
            return result;

        }

    }
}