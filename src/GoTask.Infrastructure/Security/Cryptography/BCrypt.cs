using GoTask.Domain.Interfaces.Security;
using BC = BCrypt.Net.BCrypt;

namespace GoTask.Infrastructure.Security.Cryptography;

public class BCrypt : IPasswordEncrypter
{
    public string Encrypt(string password) 
        => BC.HashPassword(password);
    

    public bool Verify(string password, string passwordHash)
        => BC.Verify(password, passwordHash);
}