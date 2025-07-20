namespace Shared.Infrastructure.Security.Jwt;

public class JwtSetting
{
    
    public string Key {get; init;}
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public double DurationInMinute { get; init; }
}