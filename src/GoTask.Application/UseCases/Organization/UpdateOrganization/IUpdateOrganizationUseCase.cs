using GoTask.Communication.Requests;

namespace GoTask.Application.UseCases.Organization.UpdateOrganization;

public interface IUpdateOrganizationUseCase
{
    Task ExecuteAsync(
        long orgId,
        OrganizationRequest request,
        string userIdentification,
        CancellationToken cancellation);
}