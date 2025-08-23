using GoTask.Communication.Requests;

namespace GoTask.Application.UseCases.User.UpdateUser;

public interface IUpdateUserUseCase
{
    System.Threading.Tasks.Task ExecuteAsync(Guid userIdentifier, UserRequest request, CancellationToken cancellationToken);
}