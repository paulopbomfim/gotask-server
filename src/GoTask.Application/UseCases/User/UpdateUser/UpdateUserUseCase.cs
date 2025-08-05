using GoTask.Application.Mapping;
using GoTask.Communication.Requests;
using GoTask.Domain.Interfaces.Repositories;
using GoTask.Exceptions.ExceptionBase;

namespace GoTask.Application.UseCases.User.UpdateUser;

public class UpdateUserUseCase(
    IUserReadOnlyRepository readOnlyRepository,
    IUserWriteOnlyRepository writeOnlyRepository,
    IUnitOfWork uow) : IUpdateUserUseCase
{
    public async Task ExecuteAsync(Guid userIdentifier, UserRequest request, CancellationToken cancellationToken)
    {
        Validate(request);

        var userToUpdate = await readOnlyRepository
            .GetUserByIdentifierAsync(userIdentifier, useTracking: true, cancellationToken)
                ?? throw new NotFoundException();
        
        userToUpdate.ApplyUpdate(request);
        userToUpdate.UpdatedAt = DateTime.UtcNow;
        
        writeOnlyRepository.UpdateUser(userToUpdate);
        await uow.Commit(cancellationToken);
    }

    private static void Validate(UserRequest request)
    {
        var validation = new UpdateUserValidator().Validate(request);
        
        if (validation.IsValid) return;

        var errorMessages = validation.Errors.Select(f => f.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}