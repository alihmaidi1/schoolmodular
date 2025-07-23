namespace Identity.Domain.Repository;

public interface IUnitOfWork:IDisposable
{

    IJwtRepository _jwtRepository { get; }
    
    IAdminRepository _adminRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}