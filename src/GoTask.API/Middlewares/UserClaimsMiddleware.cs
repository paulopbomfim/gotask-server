using System.Security.Claims;

namespace GoTask.API.Middlewares;

public sealed record UserTokenClaims(string Role, string UserIdentification);

public sealed class UserClaimsMiddleware(RequestDelegate next)
{
    private const string ItemsKey = "UserTokenClaims";

    public async Task InvokeAsync(HttpContext context)
    {
        var user = context.User;
        if (user?.Identity?.IsAuthenticated == true)
        {
            var role = user.FindFirstValue(ClaimTypes.Role);
            var sid  = user.FindFirstValue(ClaimTypes.Sid);

            if (!string.IsNullOrWhiteSpace(role) && !string.IsNullOrWhiteSpace(sid))
            {
                context.Items[ItemsKey] = new UserTokenClaims(role!, sid!);
            }
        }

        await next(context);
    }

    public static UserTokenClaims? GetUserClaims(HttpContext httpContext) =>
        httpContext.Items.TryGetValue(ItemsKey, out var value) ? value as UserTokenClaims : null;
}