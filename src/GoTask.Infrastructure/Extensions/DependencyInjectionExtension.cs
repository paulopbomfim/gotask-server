using GoTask.Domain.Interfaces.Repositories;
using GoTask.Infrastructure.DataAccess;
using GoTask.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoTask.Infrastructure.Extensions;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);

        if (!configuration.IsTestEnvironment())
            AddDbContext(services, configuration);
    }
    
    
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MySqlConnection");
        var serverVersion = ServerVersion.AutoDetect(connectionString);
        
        services.AddDbContext<GoTaskDbContext>(config => 
            config.UseMySql(connectionString, serverVersion));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        #region User

            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();

        #endregion
    }
}