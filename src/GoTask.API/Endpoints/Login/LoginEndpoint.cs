using Carter;
using GoTask.Application.UseCases.Login;
using GoTask.Communication.Requests;
using GoTask.Communication.Responses;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GoTask.API.Endpoints.Login;

public class LoginEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/login", Login)
            .AllowAnonymous()
            .WithSummary("Login da aplicação")
            .Accepts<LoginRequest>("application/json")
            .Produces<Ok<RegisterUserResponse>>()
            .Produces<ErrorResponse>(
                StatusCodes.Status401Unauthorized,
                "application/problem+json")
            .Produces<ErrorResponse>(
                StatusCodes.Status400BadRequest,
                "application/problem+json");
    }

    private static async Task<Ok<RegisterUserResponse>> Login(
        LoginRequest request,
        ILoginUseCase useCase,
        CancellationToken cancellationToken)
    {
        var response = await useCase.ExecuteAsync(request, cancellationToken);
        
        return TypedResults.Ok(response);
    }
}