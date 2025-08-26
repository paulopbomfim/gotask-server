using Carter;
using GoTask.API.Middlewares;
using GoTask.Application.UseCases.Task.List;
using GoTask.Communication.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GoTask.API.Endpoints.Task;

public class ListTasksEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/tasks/organization/{orgId:long}", ListTasksEndpointAsync);
    }

    private static async Task<Ok<IList<TasksResponse>>> ListTasksEndpointAsync(
        [FromRoute] long orgId,
        [FromQuery] long? userId,
        HttpContext httpContext,
        IListTasksUseCase useCase,
        CancellationToken ct)
    {
        var claims = UserClaimsMiddleware.GetUserClaims(httpContext);
        
        var result = await useCase.ExecuteAsync(orgId, userId, claims!.UserIdentification, ct);
        return TypedResults.Ok(result);
    }
}