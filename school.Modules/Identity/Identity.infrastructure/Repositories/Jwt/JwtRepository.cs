using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Identity.Domain.Repository;
using Identity.Domain.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Infrastructure.Security.Jwt;

namespace Identity.infrastructure.Repositories.Jwt;

public class JwtRepository: IJwtRepository
{

    private readonly schoolIdentityDbContext _context;
    private readonly JwtSetting _jwtOption;
    
    public JwtRepository(schoolIdentityDbContext context,IOptions<JwtSetting> setting)
    {
        _jwtOption = setting.Value;
        _context=context;
    }
    
    public async Task<TokenInfo> GetTokensInfo(Guid id,string email,UserType type,CancellationToken cancellationToken,List<string>? permissions=null)  
    {
        
           
        string token = GetToken(id,email,type,permissions);
        string refreshToken = GenerateRefreshToken();
        _context.RefreshTokens.Add(RefreshToken.Create(refreshToken).value!);
        await _context.SaveChangesAsync(cancellationToken);
        
        return new TokenInfo(token,refreshToken,DateTime.Now.AddMinutes(_jwtOption.DurationInMinute));
    }


    
    
    
    public string GetToken(Guid id,string email,UserType type,List<string>? permissions)
    {
        var claims = CreateClaim(id,email,type,permissions);
        var signingCredentials = GetSigningCredentials(_jwtOption);
        var jwtToken = GetJwtToken(_jwtOption, claims, signingCredentials);
        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return token;
    
        
    }
    
    private List<Claim> CreateClaim(Guid id,string email,UserType type,List<string>? permissions=null)
    {

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email,email),
            new Claim(ClaimTypes.NameIdentifier,id.ToString()),
            new Claim(ClaimTypes.Role,type.ToString())
            

        };

        if (permissions is null)
        {

            return claims;
        }
        foreach (var permission in permissions)
        {
            claims.Add(new Claim(permission,permission));
            
        }
        return claims;



    }


    
    private SigningCredentials GetSigningCredentials(JwtSetting jwtOption)
    {

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Key));
        return new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        

    }

    
    private JwtSecurityToken GetJwtToken(JwtSetting jwtOption, List<Claim> claims, SigningCredentials signingCredentials)
    {

        return new JwtSecurityToken(
            issuer: jwtOption.Issuer,
            audience: jwtOption.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(jwtOption.DurationInMinute),
            signingCredentials: signingCredentials
        );

    }
    
    
    private string GenerateRefreshToken()
    {

        var randomNumber = new byte[32];
        var generator = new RNGCryptoServiceProvider();
        generator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);


    }
    
    
}