using Carter;

namespace GoTask.API.Endpoints.Organization;

public class OrganizationBaseEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var userGroup = app.MapGroup("/api/organization");
        
        RegisterOrganizationEndpoint.AddRoute(userGroup);
    }
}