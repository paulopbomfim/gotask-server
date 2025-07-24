using Carter;

namespace GoTask.API.Endpoints;

public class UserEndpoints: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users", GetUsersListEndpoint);
            
        var group = app.MapGroup("/api/user");
        group.MapPost("", CreateUserEndpoint);
    }

    /// <summary>
    /// Endpoint para listar usuários.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static Task<IResult> GetUsersListEndpoint()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Endpoint para criação de um usuário.
    /// </summary>
    public static Task<IResult> CreateUserEndpoint()
    {
        throw new NotImplementedException();
    }
}