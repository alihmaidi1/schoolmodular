using Shared.Domain.Repositories;

namespace Identity.Domain.Repository;

public interface IIdentityUnitOfWork: IUnitOfWork
{
    
    
    
    IJwtRepository _jwtRepository { get; }
    
    IAdminRepository _adminRepository { get; }
    
}