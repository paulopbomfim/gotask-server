using GoTask.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GoTask.Infrastructure.Migrations;

public static class DataBaseMigrations 
{
    public static async Task MigrateDatabaseAsync(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<GoTaskDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}