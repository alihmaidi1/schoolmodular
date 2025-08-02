using Autofac;
using Identity.Domain.Repository;
using Identity.Domain.Services.Hash;
using Identity.infrastructure;
using Identity.infrastructure.Repositories;
using Identity.infrastructure.Repositories.Jwt;
using Identity.infrastructure.Services.Hash;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Extensions.Logging;
using Shared.Domain.Repositories;
using Shared.Infrastructure.Database;
using Shared.Infrastructure.Messages.Outbox;
using Shared.Infrastructure.Security.Jwt;

namespace Identity.Presentation;

internal class DataAccessModule: Module 
{

    
    private readonly string _databaseConnectionString;
    private readonly JwtSetting _jwtSetting;

    private readonly ILoggerFactory _loggerFactory;
    internal DataAccessModule(string databaseConnectionString,JwtSetting  jwtSetting,SerilogLoggerFactory logger)
    {
        _jwtSetting=jwtSetting;
        _loggerFactory = logger;
        _databaseConnectionString = databaseConnectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
        
        builder.RegisterType<JwtRepository>()
            .As<IJwtRepository>()
            .InstancePerLifetimeScope();
        builder.RegisterInstance(_jwtSetting)
            .As<JwtSetting>()
            .SingleInstance();

        builder.RegisterType<WordHasherService>()
            .As<IWordHasherService>()
            .InstancePerLifetimeScope();
        builder.RegisterType<AccountRepository>()
            .As<IAccountRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<AdminRepository>()
            .As<IAdminRepository>()
            .InstancePerLifetimeScope();
        
        
        builder.RegisterType<UnitOfWork>()
            .As<IIdentityUnitOfWork>()
            .As<IUnitOfWork>()
            .InstancePerLifetimeScope();


        builder.RegisterType<InsertOutboxMessagesInterceptor>()
            .AsSelf()
            .InstancePerLifetimeScope();
        
        

        
        
        builder.Register(ctx =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<SchoolIdentityDbContext>();

                var interceptor = ctx.Resolve<InsertOutboxMessagesInterceptor>();
                var schema = Schemas.Identity;

                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.UseNpgsql(
                    _databaseConnectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, schema)
                );
                optionsBuilder.AddInterceptors(interceptor);
                optionsBuilder.UseLoggerFactory(_loggerFactory);
                return new SchoolIdentityDbContext(optionsBuilder.Options);

            })
            .As<SchoolIdentityDbContext>()

            .InstancePerLifetimeScope();

    }

}