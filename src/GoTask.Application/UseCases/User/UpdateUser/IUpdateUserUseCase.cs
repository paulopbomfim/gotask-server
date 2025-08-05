using GoTask.Communication.Requests;

namespace GoTask.Application.UseCases.User.UpdateUser;

public interface IUpdateUserUseCase
{
    Task ExecuteAsync(Guid userIdentifier, UserRequest request, CancellationToken cancellationToken);
}