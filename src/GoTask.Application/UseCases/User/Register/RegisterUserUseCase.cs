using FluentValidation.Results;
using GoTask.Application.Mapping;
using GoTask.Communication.Requests;
using GoTask.Communication.Responses;
using GoTask.Domain.Interfaces.Repositories;
using GoTask.Domain.Interfaces.Security;
using GoTask.Exceptions;
using GoTask.Exceptions.ExceptionBase;

namespace GoTask.Application.UseCases.User;

public class RegisterUserUseCase(
    IUserReadOnlyRepository userReadOnlyRepository,
    IUserWriteOnlyRepository userWriteOnlyRepository,
    IPasswordEncrypter passwordEncrypter,
    IAccessTokenGenerator accessTokenGenerator,
    IUnitOfWork uow
    ) : IRegisterUserUseCase
{
    public async Task<(long userId, RegisterUserResponse userInfo)> ExecuteAsync(UserRequest request, CancellationToken cancellationToken)
    {
        await ValidateAsync(request, cancellationToken);
        
        var user = request.ToEntity();
        user.Password = passwordEncrypter.Encrypt(request.Password);

        var registeredUser = await userWriteOnlyRepository.RegisterUserAsync(user, cancellationToken);
        await uow.Commit(cancellationToken);
        
        var response = new RegisterUserResponse
        {
            Name = registeredUser.Name,
            Token = accessTokenGenerator.Generate(registeredUser),
        };
 
        return (registeredUser.Id, response);
    }

    private async System.Threading.Tasks.Task ValidateAsync(UserRequest request, CancellationToken cancellationToken)
    {
        var validation = await new RegisterUserValidator().ValidateAsync(request, cancellationToken);
        
        var activeEmailExists = await userReadOnlyRepository.ExistsActiveUserWithEmailAsync(request.Email, cancellationToken);

        if (activeEmailExists)
        {
            validation.Errors.Add(
                new ValidationFailure(
                    string.Empty,
                    ErrorMessagesResource.EMAIL_ALREADY_REGISTERED
                )
            );
        }

        if (validation.IsValid) return;

        var errorMessages = validation.Errors.Select(f => f.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}