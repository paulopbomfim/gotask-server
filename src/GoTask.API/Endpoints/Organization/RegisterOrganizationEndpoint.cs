using GoTask.API.Middlewares;
using GoTask.Application.UseCases.Organization.Register;
using GoTask.Communication.Requests;
using GoTask.Communication.Responses;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GoTask.API.Endpoints.Organization;

public static class RegisterOrganizationEndpoint
{
    public static void AddRoute(RouteGroupBuilder group)
    {
        group.MapPost("", RegisterOrganizationEndpointAsync)
            .WithName("Create organization endpoint")
            .WithSummary("Endpoint para criar uma nova organização")
            .Produces<Created<OrganizationResponse>>()
            .Produces<ErrorResponse>(
                StatusCodes.Status400BadRequest,
                "application/problem+json");
    }

    private static async Task<Created<OrganizationResponse>> RegisterOrganizationEndpointAsync(
        OrganizationRequest request,
        IRegisterOrganizationUseCase useCase,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var claims = UserClaimsMiddleware.GetUserClaims(httpContext);
        
        var response = await useCase.ExecuteAsync(request, claims!.UserIdentification, cancellationToken);
        
        return TypedResults.Created($"/api/organization/{response.orgId}", response.organizationInfo);
    }
}