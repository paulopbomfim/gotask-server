using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories.User;
using CommonTestUtilities.Request;
using CommonTestUtilities.Security;
using GoTask.Application.UseCases.Login;
using GoTask.Exceptions;
using GoTask.Exceptions.ExceptionBase;

namespace UseCases.Test.Login;

public class LoginUseCaseTest
{
    private readonly GoTask.Domain.Entities.User _user = UserBuilder.Build();

    [Fact]
    public async Task Success()
    {
        //Arrange
        var request = LoginRequestBuilder.Build();
        request.Email = _user.Email;
        var useCase = CreateUseCase(_user, request.Password);

        //Act
        var result = await useCase.ExecuteAsync(request, CancellationToken.None);
        
        //Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Token);
        Assert.Equal(_user.Name, result.Name);
    }
    
    [Fact]
    public async Task Error_User_Not_Found()
    {
        //Arrange
        var request = LoginRequestBuilder.Build();
        var useCase = CreateUseCase(_user, request.Password);
        
        //Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidLoginException>(async () =>
            await useCase.ExecuteAsync(request, CancellationToken.None));
        
        Assert.Single(exception.GetErrors());
        Assert.Equal(exception.GetErrors()[0], ErrorMessagesResource.EMAIL_OR_PASSWORD_INVALID);
    }

    
    [Fact]
    public async Task Error_Password_Not_Match()
    {
        //Arrange
        var request = LoginRequestBuilder.Build();
        request.Email = _user.Email;
        var useCase = CreateUseCase(_user);
        
        //Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidLoginException>(async () =>
            await useCase.ExecuteAsync(request, CancellationToken.None));
        
        Assert.Single(exception.GetErrors());
        Assert.Equal(exception.GetErrors()[0], ErrorMessagesResource.EMAIL_OR_PASSWORD_INVALID);
    }

    private static LoginUseCase CreateUseCase(GoTask.Domain.Entities.User user, string? password = null)
    {
        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();
        var passwordEncrypter = new PasswordEncrypterBuilder().Verify(password);
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();

        userReadOnlyRepository.GetUserByEmail(user);
        
        return new LoginUseCase(
            userReadOnlyRepository.Build(),
            passwordEncrypter.Build(),
            accessTokenGenerator);
    }
}