using GoTask.Application.UseCases.Organization.ListOrganizationTasks;
using GoTask.Communication.Responses;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GoTask.API.Endpoints.Organization;

public static class ListTasksByOrganizationEndpoint
{
    public static void AddRoute(RouteGroupBuilder group)
    {
        group.MapGet("{orgId:long}/tasks", ListTasksByOrganizationEndpointAsync);
    }

    private static async Task<Ok<IList<OrganizationTasksResponse>>> ListTasksByOrganizationEndpointAsync(
        long orgId,
        IList<long>? usersId,
        IListOrganizationTasksUseCase useCase,
        CancellationToken ct)
    {
        var result = await useCase.ExecuteAsync(orgId, usersId, ct);
        
        return TypedResults.Ok(result);
    }
}