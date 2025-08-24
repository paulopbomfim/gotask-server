using GoTask.Application.Mapping;
using GoTask.Communication.Responses;
using GoTask.Domain.Interfaces.Repositories;
using GoTask.Exceptions.ExceptionBase;

namespace GoTask.Application.UseCases.Organization.ListOrganizationTasks;

public class ListOrganizationTasksUseCase(
    IOrganizationReadOnlyRepository organizationReadOnlyRepository) : IListOrganizationTasksUseCase
{
    public async Task<IList<OrganizationTasksResponse>> ExecuteAsync(long orgId, IList<long>? usersId, CancellationToken ct)
    {
        var organization = await organizationReadOnlyRepository.GetOrganizationTasksAsync(orgId, usersId, ct)
            ?? throw new NotFoundException();
        //TODO: Arrumar mapeamento
        var organizationTasks = organization.ToUserOrganization();
        return organizationTasks;
    }
}