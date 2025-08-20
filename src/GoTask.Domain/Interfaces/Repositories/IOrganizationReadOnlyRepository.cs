using GoTask.Domain.Entities;

namespace GoTask.Domain.Interfaces.Repositories;

public interface IOrganizationReadOnlyRepository
{
    Task<Organization?> GetOrganizationWithUsersByIdAsync(long organizationId, CancellationToken cancellationToken = default);
    Task<Organization?> GetOrganizationByIdAsync(long organizationId, CancellationToken cancellationToken = default);
}