using GoTask.Domain.Interfaces.Repositories;
using Moq;

namespace CommonTestUtilities.Repositories.User;

public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository = new();
    
    public void ExistsActiveUserWithEmail(string email)
    {
        _repository
            .Setup(x=> x.ExistsActiveUserWithEmailAsync(email, CancellationToken.None))
            .ReturnsAsync(true);
    }

    public void GetUserByEmail(GoTask.Domain.Entities.User user)
    {
        _repository
            .Setup(x => x.GetUserByEmailAsync(user.Email, CancellationToken.None))
            .ReturnsAsync(user);
    }
    
    public IUserReadOnlyRepository Build() => _repository.Object;
}