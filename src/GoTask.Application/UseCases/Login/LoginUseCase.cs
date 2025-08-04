using GoTask.Communication.Requests;
using GoTask.Communication.Responses;
using GoTask.Domain.Interfaces.Repositories;
using GoTask.Domain.Interfaces.Security;
using GoTask.Exceptions.ExceptionBase;

namespace GoTask.Application.UseCases.Login;

public class LoginUseCase(
    IUserReadOnlyRepository repository,
    IPasswordEncrypter passwordEncrypter,
    IAccessTokenGenerator accessTokenGenerator) : ILoginUseCase
{
    public async Task<RegisterUserResponse> ExecuteAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        Validate(request);

        var user = await repository.GetUserByEmailAsync(request.Email, cancellationToken) 
                   ?? throw new InvalidLoginException();
        
        var passwordValid = passwordEncrypter.Verify(request.Password, user.Password);

        if (!passwordValid) throw new InvalidLoginException();
        
        return new RegisterUserResponse
        {
            Name = user.Name,
            Token = accessTokenGenerator.Generate(user),
        };
    }

    private static void Validate(LoginRequest request)
    {
        var validation = new LoginValidator().Validate(request);
        
        if (validation.IsValid) return;
        
        var errorMessages = validation.Errors.Select(x => x.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}