using Carter;

namespace GoTask.API.Endpoints.Users;

public class UserBaseEndpoint : ICarterModule
{
    /// <summary>
    /// Criação do agrupamento de rotas
    /// </summary>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var userGroup = app.MapGroup("/api/user");
        
        RegisterUserEndpoint.AddRoute(userGroup);
        GetUserEndpoint.AddRoute(userGroup);
    }
}