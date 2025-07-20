using Identity.Domain.Enum;
using Identity.Domain.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.infrastructure.Seed;

public static class DefaultRoleSeeder
{
    
    public static async Task seedData(schoolIdentityDbContext identityDbContext)
    {
        if (!await identityDbContext.Roles.AnyAsync())
        {
            identityDbContext.Roles.AddRange(Enum.GetNames(typeof(StaticRole)).Select(x=>new Role()
            {
                
                Name = x,
                NormalizedName = x.ToUpper()
                
            }).ToList());
            await identityDbContext.SaveChangesAsync();
            var superadminRole = await identityDbContext.Roles.FirstAsync(x=>x.Name==nameof(StaticRole.SuperAdmin))!;
            var permissions = Enum.GetNames(typeof(Permission)).Select(x=>new IdentityRoleClaim<Guid>
            {
                ClaimType    = "Permission",
                ClaimValue = x,
                RoleId      = superadminRole.Id,
                
            }).ToList();
            identityDbContext.RoleClaims.AddRange(permissions);
            await identityDbContext.SaveChangesAsync();
        }



    }

    
}