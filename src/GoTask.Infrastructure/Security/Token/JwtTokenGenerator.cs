using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GoTask.Domain.Entities;
using GoTask.Domain.Interfaces.Security;
using Microsoft.IdentityModel.Tokens;

namespace GoTask.Infrastructure.Security.Token;

public class JwtTokenGenerator(uint expirationTimeMinutes, string signingKey) : IAccessTokenGenerator
{
    private SymmetricSecurityKey SecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(signingKey);
        return new SymmetricSecurityKey(key);
    }
    
    public string Generate(User user)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, user.Name),
            new (ClaimTypes.Sid, user.UserIdentifier.ToString())
        };
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(securityToken);
    }
}