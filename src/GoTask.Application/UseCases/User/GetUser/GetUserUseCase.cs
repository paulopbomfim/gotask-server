using GoTask.Application.Mapping;
using GoTask.Communication.Responses;
using GoTask.Domain.Interfaces.Repositories;
using GoTask.Exceptions.ExceptionBase;

namespace GoTask.Application.UseCases.User;

public class GetUserUseCase(IUserReadOnlyRepository repository) : IGetUserUseCase
{
    public async Task<UserResponse> ExecuteAsync(Guid userIdentifier, CancellationToken ct)
    {
        var user = await repository.GetUserByIdentifierAsync(userIdentifier, cancellationToken: ct)
            ?? throw new NotFoundException();

        return user.ToResponse();
    }
}