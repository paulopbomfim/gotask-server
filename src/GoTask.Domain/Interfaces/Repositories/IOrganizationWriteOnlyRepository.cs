using GoTask.Domain.Entities;

namespace GoTask.Domain.Interfaces.Repositories;

public interface IOrganizationWriteOnlyRepository
{
    Task<Organization> RegisterOrganizationAsync(Organization organization,
        CancellationToken cancellationToken = default);
    
    void UpdateOrganization(Organization organizationToUpdate);
}