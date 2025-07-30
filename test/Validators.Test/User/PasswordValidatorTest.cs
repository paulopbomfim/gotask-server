using FluentValidation;
using GoTask.Application.UseCases;
using GoTask.Communication.Requests;

namespace Validators.Test.User;

public class PasswordValidatorTest
{
    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    [InlineData("aaaaaaaa")]
    [InlineData("Aaaaaaaa")]
    [InlineData("Aaaaaaaa1")]
    public void Error_Password_Invalid(string password)
    {
        //Arrange
        var validator = new PasswordValidation<UserRequest>();

        //Act
        var result = validator.IsValid(new ValidationContext<UserRequest>(new UserRequest()), password);

        //Assert
        Assert.False(result);
    }
}