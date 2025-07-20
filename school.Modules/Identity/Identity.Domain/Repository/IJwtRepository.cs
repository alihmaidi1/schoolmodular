using Identity.Domain.Security;
// using Shared.Authorization;

namespace Identity.Domain.Repository;

public interface IJwtRepository
{
    
    public Task<TokenInfo> GetTokensInfo(Guid id,string email,UserType type,CancellationToken cancellationToken,List<string>? permissions=null);

    public string GetToken(Guid id,string email,UserType type,List<string>? permissions);
}