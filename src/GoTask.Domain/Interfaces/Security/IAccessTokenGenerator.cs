using GoTask.Domain.Entities;

namespace GoTask.Domain.Interfaces.Security;

public interface IAccessTokenGenerator
{
    string Generate(User user);
}