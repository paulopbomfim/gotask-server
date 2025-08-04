using GoTask.Domain.Entities;
using GoTask.Domain.Interfaces.Repositories;
using Moq;

namespace CommonTestUtilities.Repositories.User;

public class UserWriteOnlyRepositoryBuilder
{
    private readonly Mock<IUserWriteOnlyRepository> _repository = new();

    public void RegisterUser()
    {
        _repository
            .Setup(x => x.RegisterUserAsync(It.IsAny<GoTask.Domain.Entities.User>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GoTask.Domain.Entities.User user, CancellationToken cancellationToken) => user);
    }
    
    public IUserWriteOnlyRepository Build() => _repository.Object;
}