using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShaGenerator.Infrastructure.Database;

namespace ShaGenerator.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        var serverVersion = new MariaDbServerVersion(new Version(12, 2, 2));

        services.AddDbContext<ShaGeneratorDbContext>(options =>
            options.UseMySql(connectionString, serverVersion));

        return services;
    }
}
