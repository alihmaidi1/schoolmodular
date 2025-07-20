namespace Identity.Domain.Security;

public class TokenInfo
{
    
    public string Token { get; private set; }
    
    public string RefreshToken { get; private set; }
    
    public DateTime ExpiresIn { get; private set; }


    public TokenInfo(string token, string refreshToken, DateTime expiresIn)
    {
        
        Token = token;
        RefreshToken = refreshToken;
        ExpiresIn = expiresIn;
    }
    
}