using Api.Extensions;
using Api.Modules;
using Api.Modules.Identity;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Shared.Infrastructure.Messages;
using ILogger = Serilog.ILogger;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new IdentityAutofacModule()); 
});




var allAssembly = AppDomain.CurrentDomain.GetAssemblies();




// Add services to the container. 
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();

});



builder.Services.AddInfrastructure(builder.Configuration,ModuleExtension.GetModuleAssemblyTypes().Keys.ToList());
builder.Services.AddApplication(ModuleExtension.GetModuleAssemblyTypes(),AppDomain.CurrentDomain.GetAssemblies().ToList());
builder.Services.AddScoped<GlobalExceptionHandlingMiddleware>();
builder.Services.AddMassTransitWithAssemblies(builder.Configuration, allAssembly);

builder.Services
    .AddIdentityApplicationModules(builder.Configuration,typeof(schoolIdentityDbContext))
    .AddIdentityInfrastructureModule(builder.Configuration)
    .AddCommonApplication();


builder.Services.AddNotificationModule(builder.Configuration);

var app = builder.Build();

// ModuleHelper.InitializeModules();
var _logger = app.Services.GetRequiredService<ILogger>();
await ModuleHelper.InitializeModules(
    builder.Configuration.GetConnectionString("DefaultConnection")!,
    _logger);

app.MapOpenApi();
app.UseInfrastructure()
    .UseApplication()
    .UseIdentityApplicationModule()
    .UseIdentityInfrastructureModule()
    .UseNotificationModule();

app.MapControllers();


app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.Run();

public partial class Program
{
    
}