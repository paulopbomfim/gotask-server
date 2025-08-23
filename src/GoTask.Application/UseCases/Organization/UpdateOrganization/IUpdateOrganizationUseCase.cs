using GoTask.Communication.Requests;

namespace GoTask.Application.UseCases.Organization.UpdateOrganization;

public interface IUpdateOrganizationUseCase
{
    System.Threading.Tasks.Task ExecuteAsync(
        long orgId,
        OrganizationRequest request,
        string userIdentification,
        CancellationToken cancellation);
}