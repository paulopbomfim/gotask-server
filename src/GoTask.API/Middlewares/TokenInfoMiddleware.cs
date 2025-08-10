using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GoTask.Exceptions.ExceptionBase;

namespace GoTask.API.Middlewares;

public sealed record TokenInfo(string Role, string UserIdentification);

public sealed class TokenInfoMiddleware(RequestDelegate next)
{
    private const string ItemsKey = "TokenInfo";

    public async Task InvokeAsync(HttpContext context)
    {
        var authHeader = context.Request.Headers.Authorization.ToString();

        if (!string.IsNullOrWhiteSpace(authHeader) &&
            authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            var jwt = authHeader["Bearer ".Length..].Trim();

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            if (token is null)
            {
                await next(context);
                return;
            }

            if (token.ValidTo < DateTime.UtcNow)
            {
                throw new UnauthorizedException();
            }

            var role = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var sid  = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

            if (!string.IsNullOrEmpty(role) && !string.IsNullOrEmpty(sid))
            {
                context.Items[ItemsKey] = new TokenInfo(role, sid);
            }
        }

        await next(context);
    }

    // Helper opcional para recuperar o contexto do usuário
    public static TokenInfo? GetUserContext(HttpContext httpContext) =>
        httpContext.Items.TryGetValue(ItemsKey, out var value) ? value as TokenInfo : null;
}