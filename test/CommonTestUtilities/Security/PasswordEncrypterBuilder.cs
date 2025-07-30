using GoTask.Domain.Interfaces.Security;
using Moq;

namespace CommonTestUtilities.Security;

public class PasswordEncrypterBuilder
{
    private readonly Mock<IPasswordEncrypter> _mock = new();

    public PasswordEncrypterBuilder()
    {
        _mock.Setup(x => x.Encrypt(It.IsAny<string>())).Returns("y-jOr=[*cMZO{ar;-ig1!fY!eXn7uWZ~");
    }
    
    public IPasswordEncrypter Build() => _mock.Object;
}