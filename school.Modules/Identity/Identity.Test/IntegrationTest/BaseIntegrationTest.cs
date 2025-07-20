// using Identity.infrastructure;
// using Identity.infrastructure.Seed;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
//
// namespace Identity.Test.IntegrationTest;
//
// public class BaseIntegrationTest: IClassFixture<IntegrationTestWebAppFactory>,IDisposable,IAsyncLifetime
// {
//     
//     private readonly IServiceScope _scope;
//     protected readonly myFoodIdentityDbContext _DbContext;
//     
//     
//     protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
//     {
//         _scope = factory.Services.CreateScope();
//         _DbContext = _scope.ServiceProvider
//             .GetRequiredService<myFoodIdentityDbContext>();
//         
//
//     }
//     
//     
//     public void Dispose()
//     {
//         _DbContext?.Dispose();
//         _scope?.Dispose();
//     }
//
//     public async Task InitializeAsync()
//     {
//         await IdentityDatabaseSeed.InitializeAsync(this._scope.ServiceProvider);
//     }
//
//     public Task DisposeAsync()
//     {
//         return Task.CompletedTask;
//         ;
//     }
// }