using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Messages;
using Shared.Infrastructure.Messages.Outbox;

namespace Shared.Infrastructure.Database;

public static class Postgres
{
    
    
    public static Action<IServiceProvider, DbContextOptionsBuilder> StandardOptions(IConfiguration configuration, string schema) =>
        (serviceProvider, options) =>
        {
            options.EnableSensitiveDataLogging();
            options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")!,
                    optionsBuilder =>
                    {
                        optionsBuilder.MigrationsHistoryTable(HistoryRepository.DefaultTableName, schema);
                    })
                .AddInterceptors(
                    serviceProvider.GetRequiredService<InsertOutboxMessagesInterceptor>());
        };
    
}