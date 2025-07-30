using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.User;
using CommonTestUtilities.Request;
using CommonTestUtilities.Security;
using GoTask.Application.UseCases.User.Register;
using GoTask.Exceptions;
using GoTask.Exceptions.ExceptionBase;

namespace UseCases.Test.User;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        //Arrange
        var request = RegisterUserRequestBuilder.Build();
        var useCase = CreateUseCase();

        //Act
        var result = await useCase.Execute(request);

        //Assert
        Assert.NotNull(result.userInfo);
        Assert.NotEmpty(result.userInfo.Name);
        Assert.NotEmpty(result.userInfo.Token);
        Assert.Equal(request.Name, result.userInfo.Name);
    }
    
    [Fact]
    public async Task Error_Email_Already_Exists()
    {
        //Arrange
        var request = RegisterUserRequestBuilder.Build();
        var useCase = CreateUseCase(request.Email);

        //Act & Assert
        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(async () => await useCase.Execute(request));
        Assert.Single(exception.GetErrors());
        Assert.Equal(exception.GetErrors()[0], ErrorMessagesResource.EMAIL_ALREADY_REGISTERED);
    }
    
    private static RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var readRepository = new UserReadOnlyRepositoryBuilder();
        var writeRepository = new UserWriteOnlyRepositoryBuilder();
        var passwordEncrypter = new PasswordEncrypterBuilder().Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var uow = UnitOfWorkBuilder.Build();
        
        if (!string.IsNullOrEmpty(email)) readRepository.ExistsActiveUserWithEmail(email);
        
        writeRepository.RegisterUser();
        
        return new RegisterUserUseCase(
            readRepository.Build(),
            writeRepository.Build(),
            passwordEncrypter,
            tokenGenerator,
            uow);
    }
}