using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Shared.Application.Services.User;

public class CurrentUserService: ICurrentUserService
{
    // public string? UserId { get; }
    // public string? UserName { get; }
    // public bool IsAuthenticated { get; }
    // public IEnumerable<Claim> Claims { get; }
    
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public string? UserId => 
        _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? UserName => 
        _httpContextAccessor.HttpContext?.User?.Identity?.Name;

    public bool IsAuthenticated => 
        _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public IEnumerable<Claim> Claims => 
        _httpContextAccessor.HttpContext?.User?.Claims ?? Enumerable.Empty<Claim>();

}