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
    public PasswordEncrypterBuilder Verify(string? password)
    {
        if (string.IsNullOrWhiteSpace(password)) return this;
        
        _mock.Setup(passwordEncrypter => passwordEncrypter.Verify(password, It.IsAny<string>())).Returns(true);

        return this;
    }
    public IPasswordEncrypter Build() => _mock.Object;
}