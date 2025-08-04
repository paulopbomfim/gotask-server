using GoTask.Communication.Responses;

namespace GoTask.Application.UseCases.User;

public interface IGetUserUseCase
{
    Task<UserResponse> ExecuteAsync(Guid userIdentifier, CancellationToken cancellationToken);
}