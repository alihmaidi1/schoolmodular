using Autofac;
using Identity.Domain.Security;
using Identity.Domain.Security.Admin;
using Identity.Domain.Services.Hash;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.infrastructure.Seed;

public static class IdentityDatabaseSeed
{


    public static async Task InitializeAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<schoolIdentityDbContext>();  
        var wordHasherService = services.GetRequiredService<IWordHasherService>();
        await context.Database.EnsureCreatedAsync();    
        var pendingMigration = await context.Database.GetPendingMigrationsAsync();
        if (!pendingMigration.Any())
        {
            await context.Database.MigrateAsync();
            
        }
        try
        {

            await DefaultUserSeeder.seedData(context,wordHasherService);


        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        
        
    }

}