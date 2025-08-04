using CommonTestUtilities.Request;
using GoTask.Application.UseCases.Login;
using GoTask.Exceptions;

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
    
    [Fact]
    public void Error_Email_Empty()
    {
        // Arrange
        var validator = new LoginValidator();
        var request = LoginRequestBuilder.Build();
        request.Email = string.Empty;
        
        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Contains(ErrorMessagesResource.EMAIL_EMPTY, result.Errors[0].ErrorMessage);
    }
    
    [Fact]
    public void Error_Password_Empty()
    {
        // Arrange
        var validator = new LoginValidator();
        var request = LoginRequestBuilder.Build();
        request.Password = string.Empty;
        
        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Contains(ErrorMessagesResource.PASSWORD_EMPTY, result.Errors[0].ErrorMessage);
    }
}