using GoTask.Communication.Responses;

namespace GoTask.Application.UseCases.Organization.GetOrganization;

public interface IGetOrganizationUseCase
{
    Task<OrganizationResponse> ExecuteAsync(long organizationId, string userIdentification, CancellationToken ct);
}