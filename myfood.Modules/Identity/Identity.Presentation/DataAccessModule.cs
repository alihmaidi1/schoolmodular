using Autofac;
using Identity.Domain.Repository;
using Identity.Domain.Services.Hash;
using Identity.infrastructure;
using Identity.infrastructure.Repositories;
using Identity.infrastructure.Repositories.Jwt;
using Identity.infrastructure.Services.Hash;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Shared.Infrastructure.Database;
using Shared.Infrastructure.Messages.Outbox;
using Shared.Infrastructure.Security.Jwt;

namespace Identity.Presentation;

internal class DataAccessModule: Module 
{

    
    private readonly string _databaseConnectionString;
    private readonly JwtSetting _jwtSetting;

    internal DataAccessModule(string databaseConnectionString,JwtSetting  jwtSetting)
    {
        _jwtSetting=jwtSetting;
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
            .As<IUnitOfWork>()
            .InstancePerLifetimeScope();


        builder.RegisterType<InsertOutboxMessagesInterceptor>()
            .AsSelf()
            .InstancePerLifetimeScope();
        
        

        
        
        builder.Register(ctx =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<schoolIdentityDbContext>();

                var interceptor = ctx.Resolve<InsertOutboxMessagesInterceptor>();
                var schema = Schemas.Identity;

                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.UseNpgsql(
                    _databaseConnectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, schema)
                );
                optionsBuilder.AddInterceptors(interceptor);

                return new schoolIdentityDbContext(optionsBuilder.Options);

            })
            .As<schoolIdentityDbContext>()

            .InstancePerLifetimeScope();

    }

}