using GoTask.API.Middlewares;
using GoTask.Application.UseCases.Task.Register;
using GoTask.Communication.Requests;
using GoTask.Communication.Responses;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GoTask.API.Endpoints.Task;

public static class RegisterTaskEndpoint
{
    public static void AddRoute(RouteGroupBuilder group)
    {
        group.MapPost("", RegisterTaskEndpointAsync)
            .WithName("Create task endpoint")
            .WithSummary("Endpoint para criar uma nova tarefa")
            .Produces<Created<TaskResponse>>()
            .Produces<ErrorResponse>(
                StatusCodes.Status400BadRequest,
                "application/problem+json");
    }

    private static async Task<Created<TaskResponse>> RegisterTaskEndpointAsync(
        TaskRequest request,
        IRegisterTaskUseCase useCase,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var claims = UserClaimsMiddleware.GetUserClaims(httpContext);
        
        var response = await useCase.ExecuteAsync(request, claims!.UserIdentification, cancellationToken);
        
        return TypedResults.Created($"/api/task/{response.taskId}", response.taskResponse);
    }
}