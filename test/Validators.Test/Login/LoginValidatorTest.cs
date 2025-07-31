using CommonTestUtilities.Request;
using GoTask.Application.UseCases.Login;

namespace Validators.Test.Login;

public class LoginValidatorTest
{
    [Fact]
    public void Success()
    {
        // Arrange
        var validator = new LoginValidator();
        var request = LoginRequestBuilder.Build();
        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.True(result.IsValid);
    }
}