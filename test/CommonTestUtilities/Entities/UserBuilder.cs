using Bogus;
using CommonTestUtilities.Security;
using GoTask.Domain.Entities;

namespace CommonTestUtilities.Entities;

public static class UserBuilder
{
    public static User Build()
    {
        var passwordEncrypter = new PasswordEncrypterBuilder().Build();
        
        return new Faker<User>()
            .RuleFor(user => user.Id, _ => 1)
            .RuleFor(user => user.Name, faker => faker.Person.FirstName)
            .RuleFor(user => user.Email, (faker, user) => faker.Internet.Email(user.Email))
            .RuleFor(user => user.Password, (_, user) => passwordEncrypter.Encrypt(user.Password))
            .RuleFor(user => user.ImageReference, faker => faker.Image.PlaceImgUrl(width: 320, height:320, category: "people"))
            .RuleFor(user => user.UserIdentifier, _ => Guid.NewGuid());
    }
}