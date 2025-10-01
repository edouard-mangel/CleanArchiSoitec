using Infrastructure;
using Infrastructure.Migrations;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using NSubstitute;


namespace AcceptanceTests.Configuration;

public static class ConfigureServices
{
    public static IServiceCollection SubstituteServices(this IServiceCollection services)
    {
        return services
            .SubstituteDatabase();
    }
    private static IServiceCollection SubstituteDatabase(this IServiceCollection services)
    {
        var databaseName = Guid.NewGuid().ToString();
        return services.AddScoped(_ => new DbContextOptionsBuilder<CreditDbContext>()
                .UseInMemoryDatabase(databaseName).Options)
                .AddSingleton(_ =>
                {
                    var substitute = Substitute.For<IMigrateDatabase>();
                    substitute.Migrate().ReturnsForAnyArgs(Task.CompletedTask);
                    return substitute;
                });
    }
}