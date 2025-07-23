namespace Identity.Application.Auth.Admin.Command.Login;

public class LoginAdminResponse
{
    public string Token { get; set; }
    
    public string RefreshToken { get; set; }
    
    public DateTime ExpiresIn { get; set; }
    
    public List<string> permissions { get; set; } 
}