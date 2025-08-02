using Identity.Domain.Enum;
using Identity.Domain.Security;
using Identity.Domain.Security.Admin;
using Identity.Domain.Services.Hash;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.infrastructure.Seed;

public class DefaultUserSeeder
{
    
    public static async Task seedData(SchoolIdentityDbContext dbContext,IWordHasherService wordHasherService)
    {
        
        if (!dbContext.Admins.Any())
        {
            var role = Role.Create(nameof(StaticRole.SuperAdmin),Enum.GetNames(typeof(Permission)).ToList());
            
            var superAdmin = Admin.Create(nameof(StaticRole.SuperAdmin)+"@gmail.com", "12345678",role,wordHasherService);
            
            dbContext.Admins.Add(superAdmin);
            await dbContext.SaveChangesAsync();
        }

        
    }

}