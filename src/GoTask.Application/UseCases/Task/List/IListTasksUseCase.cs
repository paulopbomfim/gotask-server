using GoTask.Communication.Responses;

namespace GoTask.Application.UseCases.Task.List;

public interface IListTasksUseCase
{
    Task<IList<TasksResponse>> ExecuteAsync(long orgId, long? userId, string userIdentification, CancellationToken ct);
}