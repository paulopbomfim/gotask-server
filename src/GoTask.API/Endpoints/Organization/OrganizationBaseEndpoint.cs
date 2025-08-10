using Carter;

namespace GoTask.API.Endpoints.Organization;

public class OrganizationBaseEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var organizationGroup = app.MapGroup("/api/organization");
        
        RegisterOrganizationEndpoint.AddRoute(organizationGroup);
        GetOrganizationEndpoint.AddRoute(organizationGroup);
    }
}