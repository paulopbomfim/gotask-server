namespace GoTask.Domain.Interfaces.Security;

public interface IPasswordEncrypter
{
    string Encrypt(string password);
    bool Verify(string password, string passwordHash);
}