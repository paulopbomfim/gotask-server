using GoTask.API.Middlewares;
using GoTask.Application.UseCases.Organization.GetOrganization;
using GoTask.Communication.Responses;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GoTask.API.Endpoints.Organization;

public static class GetOrganizationEndpoint
{
    public static void AddRoute(RouteGroupBuilder group)
    {
        group.MapGet("{orgId:long}", GetOrganizationEndpointAsync);
    }

    private static async Task<Ok<OrganizationResponse>> GetOrganizationEndpointAsync(
        long orgId,
        HttpContext httpContext,
        IGetOrganizationUseCase useCase,
        CancellationToken cancellationToken)
    {
        var tokenInfo = TokenInfoMiddleware.GetUserContext(httpContext);
        
        var result = await useCase.ExecuteAsync(orgId, tokenInfo, cancellationToken);
        
        return TypedResults.Ok(result);
    }
}