using GoTask.Application.UseCases.User.UpdateUser;
using GoTask.Communication.Requests;
using GoTask.Communication.Responses;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GoTask.API.Endpoints.Users;

public class UpdateUserEndpoint
{
    public static void AddRoute(RouteGroupBuilder group)
    {
        group.MapPut("{userIdentifier:guid}", UpdateUserEndpointAsync)
            .WithName("Update user endpoint")
            .WithSummary("Endpoint para edição dos dados básicos de um usuário.")
            .Accepts<UserRequest>("application/json")
            .Produces<NoContent>()
            .Produces<ErrorResponse>(
                StatusCodes.Status400BadRequest,
                "application/problem+json")
            .Produces<ErrorResponse>(
                StatusCodes.Status401Unauthorized,
                "application/problem+json");
    }

    private static async Task<NoContent> UpdateUserEndpointAsync(Guid userIdentifier, UserRequest request, IUpdateUserUseCase useCase, CancellationToken cancellationToken)
    {
        await useCase.ExecuteAsync(userIdentifier, request, cancellationToken);

        return TypedResults.NoContent();
    }
}