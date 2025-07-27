using System.Reflection;
using Identity.Domain.Security;
using Identity.Domain.Security.Admin;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Database;
using Shared.Infrastructure.Messages;
using Shared.Infrastructure.Messages.Inbox;
using Shared.Infrastructure.Messages.Outbox;

namespace Identity.infrastructure;

public class schoolIdentityDbContext: DbContext
{
    
    
    public schoolIdentityDbContext(DbContextOptions<schoolIdentityDbContext> option) : base(option)
    {


    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(Schemas.Identity);
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyConfiguration(new OutboxMessageConfiguration());
        builder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        builder.ApplyConfiguration(new InboxMessageConfiguration());
        builder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        base.OnModelCreating(builder);

    }
    
    
    public DbSet<RefreshToken> RefreshTokens { get; init; }
    
    public DbSet<Admin> Admins { get; init; }
    
    public DbSet<Role> Roles { get; init; }
    

}