using CommonTestUtilities.Request;
using GoTask.Application.UseCases.User;
using GoTask.Exceptions;

namespace Validators.Test.User;

public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        //Assert
        var validator = new RegisterUserValidator();
        var request = RegisterUserRequestBuilder.Build();
        
        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    public void Error_Name_Empty(string name)
    {
        //Assert
        var validator = new RegisterUserValidator();
        var request = RegisterUserRequestBuilder.Build();
        request.Name = name;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ErrorMessagesResource.NAME_EMPTY, result.Errors[0].ErrorMessage);
    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    public void Error_Email_Empty(string email)
    {
        //Assert
        var validator = new RegisterUserValidator();
        var request = RegisterUserRequestBuilder.Build();
        request.Email = email;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        Assert.False(result.IsValid);
        Assert.Contains(ErrorMessagesResource.EMAIL_EMPTY, result.Errors[0].ErrorMessage);
    }

    [Fact]
    public void Error_Email_Invalid()
    {
        //Assert
        var validator = new RegisterUserValidator();
        var request = RegisterUserRequestBuilder.Build();
        request.Email = "JohnDoe.com";
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ErrorMessagesResource.EMAIL_INVALID, result.Errors[0].ErrorMessage);
    }
}