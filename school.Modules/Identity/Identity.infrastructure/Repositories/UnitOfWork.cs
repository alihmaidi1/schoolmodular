using Identity.Domain.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace Identity.infrastructure.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly schoolIdentityDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(IAdminRepository adminRepository,IJwtRepository jwtRepository,schoolIdentityDbContext context)
    {
        _context = context;
        _jwtRepository = jwtRepository;
        _adminRepository = adminRepository;
    }

    public IJwtRepository _jwtRepository { get; }
    public IAdminRepository _adminRepository { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public async Task BeginTransactionAsync()
        => _transaction = await _context.Database.BeginTransactionAsync();

    public async Task CommitTransactionAsync()
    {
        await _context.SaveChangesAsync();
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
        }
    }





    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }


    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
        if (_transaction != null) await _transaction.DisposeAsync();
    }
}