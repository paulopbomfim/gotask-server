using GoTask.API.Middlewares;
using GoTask.Application.UseCases.Organization.UpdateOrganization;
using GoTask.Communication.Requests;
using GoTask.Communication.Responses;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GoTask.API.Endpoints.Organization;

public static class UpdateOrganizationEndpoint
{
    public static void AddRoute(RouteGroupBuilder group)
    {
        group.MapPut("{orgId:long}", UpdateOrganizationEndpointAsync)
            .WithName("Update organization endpoint")
            .WithSummary("Endpoint para atualizar uma organização")
            .WithDescription("O endpoint atualiza uma organização, mas só pode ser acessado por usuários que fazem parte da organização.")
            .Produces<Ok>()
            .Produces<ErrorResponse>(
                StatusCodes.Status404NotFound,
                "application/problem+json")
            .Produces<ErrorResponse>(
                StatusCodes.Status401Unauthorized,
                "application/problem+json");;
    }

    private static async Task<Ok> UpdateOrganizationEndpointAsync(
        long orgId,
        OrganizationRequest request,
        HttpContext httpContext,
        IUpdateOrganizationUseCase useCase,
        CancellationToken cancellationToken)
    {
        var claims = UserClaimsMiddleware.GetUserClaims(httpContext);
        
        await useCase.ExecuteAsync(orgId, request, claims!.UserIdentification, cancellationToken);
        
        return TypedResults.Ok();
    }
}