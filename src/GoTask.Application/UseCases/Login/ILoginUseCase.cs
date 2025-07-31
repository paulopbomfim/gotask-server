using GoTask.Communication.Requests;
using GoTask.Communication.Responses;

namespace GoTask.Application.UseCases.Login;

public interface ILoginUseCase
{
    Task<RegisterUserResponse> Execute(LoginRequest request);
}