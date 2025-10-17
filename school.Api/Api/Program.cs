using Carter;
using Shared.Application.Extensions;
using Shared.Application.Versioning;
using Shared.Infrastructure.Security;


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, config) =>config.ReadFrom.Configuration(context.Configuration));
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new IdentityAutofacModule()); 
});

// Add services to the container. 
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();

});
builder.Services.AddScoped<GlobalExceptionHandlingMiddleware>();


builder.Services.AddLimitRate();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddOptions<JwtSetting>()
    .BindConfiguration("Jwt")
    .ValidateDataAnnotations()
    .ValidateOnStart();
builder.Services.AddVersioning();
builder.Services.AddCarterWithAssemblies(ModuleExtension.GetModuleAssemblyTypes().Values.ToArray());

var app = builder.Build();

var _logger = app.Services.GetRequiredService<ILogger>();
var jwtSetting = app.Services.GetRequiredService<IOptions<JwtSetting>>();

await ModuleHelper.InitializeModules(
    builder.Configuration.GetConnectionString("DefaultConnection")!,
    _logger,
    jwtSetting.Value);




app.MapOpenApi();
app.UseRateLimiter();
//
app.UseAuthentication();

app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "School Management API");
            
    });
app.MapCarter();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.Run();

public partial class Program
{
    
}