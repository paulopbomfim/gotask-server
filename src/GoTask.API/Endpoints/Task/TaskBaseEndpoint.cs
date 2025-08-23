using Carter;

namespace GoTask.API.Endpoints.Task;

public class TaskBaseEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/task");
        
        RegisterTaskEndpoint.AddRoute(group);
    }
}