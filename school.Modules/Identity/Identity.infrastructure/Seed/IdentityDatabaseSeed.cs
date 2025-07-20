using Identity.Domain.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.infrastructure.Seed;

public static class IdentityDatabaseSeed
{


    public static async Task InitializeAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<schoolIdentityDbContext>();     
        var roleManager = services.GetRequiredService<RoleManager<Role>>();     
        var userManager = services.GetRequiredService<UserManager<User>>();     
        await context.Database.EnsureCreatedAsync();    
        var pendingMigration = await context.Database.GetPendingMigrationsAsync();
        if (!pendingMigration.Any())
        {
            await context.Database.MigrateAsync();
            
        }
        try
        {

            await DefaultRoleSeeder.seedData(context);
            await DefaultUserSeeder.seedData(userManager);


        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        
        
    }

}