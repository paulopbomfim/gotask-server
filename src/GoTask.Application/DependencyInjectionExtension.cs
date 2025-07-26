using GoTask.Application.UseCases.User.Register;
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
        #region Users

        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();

        #endregion
    }
}

