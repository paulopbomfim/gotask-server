using GoTask.Communication.Responses;

namespace GoTask.Application.UseCases.Organization.GetOrganization;

public interface IGetOrganizationUseCase
{
    Task<OrganizationResponse> ExecuteAsync(Guid userIdentifier, long organizationId, CancellationToken ct);
}