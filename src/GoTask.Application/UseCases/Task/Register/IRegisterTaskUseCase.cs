using GoTask.Communication.Requests;
using GoTask.Communication.Responses;

namespace GoTask.Application.UseCases.Task.Register;

public interface IRegisterTaskUseCase
{
    Task<(long taskId, TaskResponse taskResponse)> ExecuteAsync(TaskRequest request, string userIdentification, CancellationToken ct);
}