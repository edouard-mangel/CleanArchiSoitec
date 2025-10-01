using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace AcceptanceTests.Configuration;

public static class ConfigureServices
{

    private static IServiceCollection SubstituteDatabase(this IServiceCollection services)
    {
        var databaseName = Guid.NewGuid().ToString();
        return services.AddScoped(_ => new DbContextOptionsBuilder<CreditDbContext>()
                .UseInMemoryDatabase(databaseName).Options);
    }
}