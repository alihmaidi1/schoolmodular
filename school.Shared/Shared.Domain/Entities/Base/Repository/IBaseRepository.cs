namespace Shared.Domain.Entities.Base.Repository;

public interface IBaseRepository <TEntity> where TEntity : IEntity
{
    
    
    Task<TEntity?> GetByIdAsync(Guid id);
    
    IQueryable<TEntity> GetQueryable();
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id);

    
}