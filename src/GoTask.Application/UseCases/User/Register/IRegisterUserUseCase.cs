using GoTask.Communication.Requests;
using GoTask.Communication.Responses;

namespace GoTask.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    Task<(long userId, RegisterUserResponse userInfo)> Execute(UserRequest request);
}