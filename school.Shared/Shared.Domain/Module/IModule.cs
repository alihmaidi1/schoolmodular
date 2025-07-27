using Shared.Domain.MediatR;

namespace Shared.Domain.Module;

public interface IModule
{
    
    Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

    Task ExecuteCommandAsync(ICommand command);

    Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    
}