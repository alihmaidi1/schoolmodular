using Identity.Domain.Repository;
using Identity.Domain.Security;
using Shared.Infrastructure.Repositories;

namespace Identity.infrastructure.Repositories;

public class AccountRepository: BaseRepository<User>,IAccountRepository
{
    public AccountRepository(schoolIdentityDbContext context) : base(context)
    {
    }
}