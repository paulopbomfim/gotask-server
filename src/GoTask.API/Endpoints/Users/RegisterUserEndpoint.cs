using GoTask.Application.UseCases.User;
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
        
        group.MapPost("", RegisterUserEndpointAsync)
            .AllowAnonymous()
            .WithName("Create user endpoint")
            .WithSummary("Endpoint para criação de um usuário.")
            .Accepts<UserRequest>("application/json")
            .Produces<Created<RegisterUserResponse>>()
            .Produces<ErrorResponse>(
                StatusCodes.Status400BadRequest,
                "application/problem+json");

    }
    
    private static async Task<Created<RegisterUserResponse>> RegisterUserEndpointAsync(
        UserRequest request,
        IRegisterUserUseCase useCase,
        CancellationToken cancellationToken)
    {
        var response = await useCase.ExecuteAsync(request, cancellationToken);

        return TypedResults.Created($"/api/user/{response.userId}", response.userInfo);
    }
}