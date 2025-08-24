using GoTask.Communication.Responses;

namespace GoTask.Application.UseCases.Organization.ListOrganizationTasks;

public interface IListOrganizationTasksUseCase
{
    Task<IList<OrganizationTasksResponse>> ExecuteAsync(long orgId, IList<long>? usersId, CancellationToken ct);
}