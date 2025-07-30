using GoTask.Domain.Interfaces.Repositories;
using Moq;

namespace CommonTestUtilities.Repositories.User;

public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository = new();
    
    public void ExistsActiveUserWithEmail(string email)
    {
        _repository
            .Setup(x=> x.ExistsActiveUserWithEmail(email))
            .ReturnsAsync(true);
    }
    
    public IUserReadOnlyRepository Build() => _repository.Object;
}