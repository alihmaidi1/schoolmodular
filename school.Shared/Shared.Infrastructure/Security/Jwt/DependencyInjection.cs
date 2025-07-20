using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Shared.Infrastructure.Security.Jwt;

public static class DependencyInjection
{

    public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration Configuration)
    {
        var jwtOption = Configuration.GetSection("Jwt");
        
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption["Key"])),
                ValidAudience = jwtOption["Audience"],
                ValidIssuer = jwtOption["Issuer"],
        
                
        
        
            };
        
        
        });
        services.AddAuthorization(options =>
        {

            options.DefaultPolicy=new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
            
        });
        return services;
    }
    
}