using GoTask.Application.UseCases.User;
using GoTask.Communication.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

namespace GoTask.API.Endpoints.Users;

public static class GetUserEndpoint
{
    public static void AddRoute(RouteGroupBuilder group)
    {
        group.MapPost("{userIdentifier:guid}", GetUserEndpointAsync)
            .WithName("Get user endpoint")
            .WithSummary("Endpoint para retornar o usuário definido.")
            .Produces<UserResponse>()
            .Produces<ErrorResponse>(
                StatusCodes.Status404NotFound,
                "application/problem+json");
    }

    private static async Task<Ok<UserResponse>> GetUserEndpointAsync(
        Guid userIdentifier,
        IGetUserUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(userIdentifier, cancellationToken);
        
        return TypedResults.Ok(result);
    }
}