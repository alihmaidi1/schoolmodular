using Identity.Domain.Repository;
using Identity.Domain.Security;
using Identity.Domain.Security.Admin;
using Shared.Infrastructure.Repositories;

namespace Identity.infrastructure.Repositories;

public class AccountRepository: BaseRepository<User>,IAccountRepository
{
    public AccountRepository(SchoolIdentityDbContext context) : base(context)
    {
    }
}