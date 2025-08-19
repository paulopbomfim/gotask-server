using GoTask.API.Middlewares;
using GoTask.Application.UseCases.Organization.GetOrganization;
using GoTask.Communication.Responses;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GoTask.API.Endpoints.Organization;

public static class GetOrganizationWithUsersEndpoint
{
    public static void AddRoute(RouteGroupBuilder group)
    {
        group.MapGet("{orgId:long}", GetOrganizationWithUsersEndpointAsync)
            .WithName("Get organization by id with users endpoint")
            .WithSummary("Endpoint para buscar uma organização com os usuários pelo id")
            .WithDescription("O endpoint busca a organização com usuários pelo id, mas só encontra se o usuário logado fizer parte da organização.")
            .Produces<Ok<OrganizationResponse>>()
            .Produces<ErrorResponse>(
                StatusCodes.Status404NotFound,
                "application/problem+json");
    }

    private static async Task<Ok<OrganizationResponse>> GetOrganizationWithUsersEndpointAsync(
        long orgId,
        HttpContext httpContext,
        IGetOrganizationUseCase useCase,
        CancellationToken cancellationToken)
    {
        var claims = UserClaimsMiddleware.GetUserClaims(httpContext);
        
        var result = await useCase.ExecuteAsync(orgId, claims!.UserIdentification, cancellationToken);
        return TypedResults.Ok(result);
    }
}