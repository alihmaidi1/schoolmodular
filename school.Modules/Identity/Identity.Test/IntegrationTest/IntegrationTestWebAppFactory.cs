// using Identity.infrastructure;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Mvc.Testing;
// using Microsoft.AspNetCore.TestHost;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
// using Testcontainers.PostgreSql;
//
// namespace Identity.Test.IntegrationTest;
//
// public class IntegrationTestWebAppFactory: WebApplicationFactory<Program>,IAsyncLifetime
// {
//     
//     private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
//         .WithImage("postgres:alpine")
//         .WithUsername("postgres")
//         .WithEnvironment("ASPNETCORE_ENVIRONMENT","Development")
//         .WithPassword("Strongpassword123!")
//         .WithCleanUp(true)
//         .Build();
//     protected override void ConfigureWebHost(IWebHostBuilder builder)
//     {
//         
//
//         builder.ConfigureTestServices(Services =>
//         {
//             
//             var descriptor= Services.SingleOrDefault(x=>x.ServiceType == typeof(DbContextOptions<myFoodIdentityDbContext>));
//             if (descriptor is not null)
//             {
//                 Services.Remove(descriptor);
//             }
//             Services.AddDbContext<myFoodIdentityDbContext>(options =>
//             {
//
//                 options.UseNpgsql(_dbContainer.GetConnectionString());
//             });
//             
//             
//             
//             
//
//         });
//
//     }
//
//     public Task InitializeAsync()
//     {
//         return _dbContainer.StartAsync();
//
//     }
//
//     public new Task DisposeAsync()
//     {
//         return _dbContainer.StopAsync();
//
//     }
// }