using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShaGenerator.Application.Hashes;
using ShaGenerator.Infrastructure.Database;
using ShaGenerator.Infrastructure.Messaging;

namespace ShaGenerator.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddMessaging(configuration);
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

    private static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqOptions>(configuration.GetSection(RabbitMqOptions.SectionName));
        services.AddScoped<IHashPublisher, RabbitMqHashPublisher>();
        return services;
    }
}
