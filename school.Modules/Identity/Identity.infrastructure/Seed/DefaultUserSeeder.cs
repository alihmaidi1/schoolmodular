using Identity.Domain.Enum;
using Identity.Domain.Security;
using Microsoft.AspNetCore.Identity;

namespace Identity.infrastructure.Seed;

public class DefaultUserSeeder
{
    
    public static async Task seedData(UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var defaultUser = new User()
            {
                UserName = nameof(StaticRole.SuperAdmin),
                Email = nameof(StaticRole.SuperAdmin)+"@gmail.com",
                EmailConfirmed = true,
                UserType = UserType.Admin,
                
                
                
            };
            await userManager.CreateAsync(defaultUser, "StrongPassword123!");
            await userManager.AddToRoleAsync(defaultUser, nameof(StaticRole.SuperAdmin));
        }
        
    }

}