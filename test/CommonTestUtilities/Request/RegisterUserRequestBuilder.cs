using Bogus;
using GoTask.Communication.Requests;

namespace CommonTestUtilities.Request;

public class RegisterUserRequestBuilder
{
    public static UserRequest Build()
    {
        return new Faker<UserRequest>()
            .RuleFor(x => x.Name, f => f.Name.FullName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Password, f => f.Internet.Password(prefix: "!Aa1"))
            .RuleFor(x => x.ImageReference, f => f.Image.PlaceImgUrl(width: 320, height:320, category: "people"));
    }
}