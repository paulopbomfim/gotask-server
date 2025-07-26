using FluentValidation.Results;
using GoTask.Application.Mapping;
using GoTask.Communication.Requests;
using GoTask.Communication.Responses;
using GoTask.Domain.Interfaces.Repositories;
using GoTask.Exceptions;
using GoTask.Exceptions.ExceptionBase;

namespace GoTask.Application.UseCases.User.Register;

public class RegisterUserUseCase(
    IUserReadOnlyRepository userReadOnlyRepository,
    IUserWriteOnlyRepository userWriteOnlyRepository,
    IUnitOfWork uow
    ) : IRegisterUserUseCase
{
    public async Task<(long userId, RegisterUserResponse userInfo)> Execute(UserRequest request)
    {
        await Validate(request);
        
        var user = new UserMappingProfile().ToEntity(request);

        var registeredUser = await userWriteOnlyRepository.RegisterUser(user);
        await uow.Commit();
        
        var response = new RegisterUserResponse { Name = registeredUser.Name, Token = "teste" };
 
        return (registeredUser.Id, response);
    }

    private async Task Validate(UserRequest request)
    {
        var validation = await new RegisterUserValidation().ValidateAsync(request);
        
        var activeEmailExists = await userReadOnlyRepository.ExistsActiveUserWithEmail(request.Email);

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