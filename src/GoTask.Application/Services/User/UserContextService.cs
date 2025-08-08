using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IdentityModel;
using GoTask.Communication.Enums;
using GoTask.Exceptions.ExceptionBase;
using Microsoft.AspNetCore.Http;

namespace GoTask.Application.Services.User;

public class UserContextService : IUserContextService
{
    private string _loadedJwt;

    public required string Role { get; set; }
    public required string UserIdentification { get; set; }
    
    
    public void LoadDataFromContext()
    {
        var httpContext = new HttpContextAccessor().HttpContext;

        var headers = httpContext?.Request.Headers;

        if (headers?.TryGetValue("Authorization", out var values) != true) return;
        
        var jwt = values.First()!.Replace("Bearer ", string.Empty);
        Load(jwt, headers);
    }

    public IUserContextService Load(string jwt, IHeaderDictionary headers = null)
    {
        if (_loadedJwt == jwt)
        {
            return this;
        }

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);

        if (token == null) return this;

        if (token.ValidTo < DateTime.Now) throw new UnauthorizedException();

        Role = token.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).First();
        UserIdentification = token.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).First();

        return this;
    }
}