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
builder.Services.AddApplication(ModuleExtension.GetModuleAssemblyTypes());
builder.Services.AddScoped<GlobalExceptionHandlingMiddleware>();
builder.Services.AddMassTransitWithAssemblies(builder.Configuration, allAssembly);

builder.Services
    .AddCommonApplication();


builder.Services.AddNotificationModule(builder.Configuration);

var app = builder.Build();

var _logger = app.Services.GetRequiredService<ILogger>();
var jwtSetting = app.Services.GetRequiredService<IOptions<JwtSetting>>();
await ModuleHelper.InitializeModules(
    builder.Configuration.GetConnectionString("DefaultConnection")!,
    _logger,
    jwtSetting.Value);

app.MapOpenApi();
app.UseInfrastructure()
    .UseApplication()
    .UseNotificationModule();

app.MapControllers();


app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.Run();

public partial class Program
{
    
}