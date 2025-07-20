using Shared.Domain.Entities;
using Shared.Domain.OperationResult;

namespace Shared.Infrastructure.Services.Archive;

public interface IArchiveService
{
    public Task<TResult<bool>> ArchiveEntityAsync<TEntity>(Guid Id, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
       where TEntity : class, IEntity;

    public Task<TResult<TEntity>> RestoreEntityAsync<TEntity>(Guid id)
        where TEntity : class, IEntity;

    public Task<TResult<bool>> DeleteArchiveAsync<TEntity>(Guid id)
        where TEntity : class, IEntity;

    public Task<TResult<List<TEntity>>> GetArchivesAsync<TEntity>()
        where TEntity : class, IEntity;


}