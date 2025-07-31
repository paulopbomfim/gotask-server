using Bogus;
using GoTask.Communication.Requests;

namespace CommonTestUtilities.Request;

public class LoginRequestBuilder
{
    public static LoginRequest Build()
    {
        return new Faker<LoginRequest>()
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Password, f => f.Internet.Password(prefix: "!Aa1"));
    }
}