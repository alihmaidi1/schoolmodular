using Identity.Domain.Repository;
using Identity.Domain.Security.Admin;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Identity.infrastructure.Repositories;

public class AdminRepository:BaseRepository<Admin>,IAdminRepository
{
    public AdminRepository(schoolIdentityDbContext context) : base(context)
    {
    }
}