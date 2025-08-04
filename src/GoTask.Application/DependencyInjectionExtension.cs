using GoTask.Application.UseCases.Login;
using GoTask.Application.UseCases.User;
using Microsoft.Extensions.DependencyInjection;

namespace GoTask.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        
        #region Users

        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IGetUserUseCase, GetUserUseCase>();

        #endregion
    }
}

