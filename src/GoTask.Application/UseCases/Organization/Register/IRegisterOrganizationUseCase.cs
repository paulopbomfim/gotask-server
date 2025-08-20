using GoTask.Communication.Requests;
using GoTask.Communication.Responses;

namespace GoTask.Application.UseCases.Organization.Register;

public interface IRegisterOrganizationUseCase
{
    Task<(long orgId, OrganizationResponse organizationInfo)> ExecuteAsync(
        OrganizationRequest request,
        string userIdentification,
        CancellationToken ct);
}