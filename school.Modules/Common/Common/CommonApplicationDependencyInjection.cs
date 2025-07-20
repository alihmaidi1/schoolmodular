using System.Reflection;
using Carter;
using Common.Services.File;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public static class CommonApplicationDependencyInjection
{

    public static IServiceCollection AddCommonApplication(this IServiceCollection services)
    {
        
        services.AddScoped<IAwsStorageService,AwsStorageService>();
        
        services.AddOptions<AwsS3Setting>()
            .BindConfiguration("AwsS3")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

}