using GoTask.Communication.Requests;
using GoTask.Communication.Responses;

namespace GoTask.Application.UseCases.User;

public interface IRegisterUserUseCase
{
    Task<(long userId, RegisterUserResponse userInfo)> ExecuteAsync(UserRequest request, CancellationToken cancellationToken);
}