namespace GoTask.Application.UseCases.Organization.GetOrganization;

public interface IGetOrganizationUseCase
{
    Task ExecuteAsync(Guid userIdentifier, long organizationId, CancellationToken ct);
}