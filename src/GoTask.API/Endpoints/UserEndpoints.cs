using Carter;
using GoTask.Application.UseCases.User.Register;
using GoTask.Communication.Requests;
using GoTask.Communication.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
    private static Task<IResult> GetUsersListEndpoint()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Endpoint para criação de um usuário.
    /// </summary>
    private static async Task<Created<RegisterUserResponse>> CreateUserEndpoint([FromBody] UserRequest request, IRegisterUserUseCase useCase)
    {
        var response = await useCase.Execute(request);

        return TypedResults.Created($"/api/user/{response.userId}", response.userInfo);
    }
}