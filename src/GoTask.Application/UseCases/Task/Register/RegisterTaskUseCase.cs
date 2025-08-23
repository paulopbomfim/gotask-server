using GoTask.Application.Mapping;
using GoTask.Communication.Requests;
using GoTask.Communication.Responses;
using GoTask.Domain.Interfaces.Repositories;
using GoTask.Exceptions.ExceptionBase;

namespace GoTask.Application.UseCases.Task.Register;

public class RegisterTaskUseCase(
    ITasksWriteOnlyRepository tasksWriteOnlyRepository,
    IUserReadOnlyRepository userReadOnlyRepository,
    IUnitOfWork uow) : IRegisterTaskUseCase
{
    public async Task<(long taskId, TaskResponse taskResponse)> ExecuteAsync(TaskRequest request, string userIdentification, CancellationToken ct)
    {
        Validate(request);
        
        var userId = Guid.Parse(userIdentification);
        var user = await userReadOnlyRepository.GetUserByIdentifierAsync(userId, cancellationToken: ct)
            ?? throw new UnauthorizedException();

        var newTask = request.ToEntity();
        newTask.UserId = user.Id;

        var task = await tasksWriteOnlyRepository.RegisterTaskAsync(newTask, ct);
        await uow.Commit(ct);
        
        return (task.Id, task.ToResponse());
    }

    private static void Validate(TaskRequest request)
    {
        var validation = new TaskValidator().Validate(request);

        if (validation.IsValid) return;
        
        var errorMessages = validation.Errors.Select(f=> f.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}