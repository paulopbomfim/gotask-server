using GoTask.Application.UseCases.User.Register;
using GoTask.Communication.Requests;
using GoTask.Communication.Responses;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GoTask.API.Endpoints.Users;

public static class RegisterUserEndpoint
{
    /// <summary>
    /// Agrupamento de rotas
    /// </summary>
    public static void AddRoute(RouteGroupBuilder group)
    {
        
        group.MapPost("", CreateUserEndpoint)
            .WithSummary("Endpoint para criação de um usuário.")
            .WithName("CreateUserEndpoint")
            .Accepts<UserRequest>("application/json")
            .Produces<Created<RegisterUserResponse>>()
            .Produces<ErrorResponse>(
                StatusCodes.Status400BadRequest,
                "application/problem+json");

    }
    
    private static async Task<Created<RegisterUserResponse>> CreateUserEndpoint(UserRequest request, IRegisterUserUseCase useCase)
    {
        var response = await useCase.Execute(request);

        return TypedResults.Created($"/api/user/{response.userId}", response.userInfo);
    }
}