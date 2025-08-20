using GoTask.Application.Services.User;
using GoTask.Application.UseCases.Login;
using GoTask.Application.UseCases.Organization.GetOrganization;
using GoTask.Application.UseCases.Organization.Register;
using GoTask.Application.UseCases.Organization.UpdateOrganization;
using GoTask.Application.UseCases.User;
using GoTask.Application.UseCases.User.UpdateUser;
using Microsoft.Extensions.DependencyInjection;

namespace GoTask.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        AddServices(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        
        #region Users

        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IGetUserUseCase, GetUserUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();

        #endregion
        
        #region Organization
        
        services.AddScoped<IRegisterOrganizationUseCase, RegisterOrganizationUseCase>();
        services.AddScoped<IGetOrganizationUseCase, GetOrganizationUseCase>();
        services.AddScoped<IUpdateOrganizationUseCase, UpdateOrganizationUseCase>();
        
        #endregion
    }
    
    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IUserContextService, UserContextService>();
    }
}

