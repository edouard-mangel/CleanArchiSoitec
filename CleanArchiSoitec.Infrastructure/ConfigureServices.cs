using Infrastructure.Migrations;

using Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


public static class ConfigureServices
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<CreditDbContext>(p => p.UseSqlite(configuration.GetConnectionString("Database")))
            .AddSingleton<IMigrateDatabase, DatabaseMigration>();
    }
}