using GoTask.Application.Mapping;
using GoTask.Communication.Responses;
using GoTask.Domain.Interfaces.Repositories;
using GoTask.Exceptions.ExceptionBase;

namespace GoTask.Application.UseCases.Task.List;

public class ListTasksUseCase(
    ITasksReadOnlyRepository repository,
    IUserReadOnlyRepository userRepository) : IListTasksUseCase
{
    public async Task<IList<TasksResponse>> ExecuteAsync(long orgId, long? userId, string userIdentification, CancellationToken ct)
    {
        var userGuid = Guid.Parse(userIdentification);
        var loggedUser = await userRepository.GetUserByIdentifierAsync(userGuid, cancellationToken: ct);

        if (loggedUser is null || loggedUser.OrganizationId != orgId) throw new NotFoundException();
        
        var tasks = await repository.GetTasksAsync(orgId, userId, ct);

        return tasks.ToTasksResponse();
    }
}