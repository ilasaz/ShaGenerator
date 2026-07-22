using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShaGenerator.Application.Abstractions.Data;
using ShaGenerator.Application.Hashes;
using ShaGenerator.Application.Services;
using ShaGenerator.Infrastructure.Database;
using ShaGenerator.Infrastructure.Messaging;

namespace ShaGenerator.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddMessaging(configuration);
        services.AddScoped<HashGeneratorService>();
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        var serverVersion = new MariaDbServerVersion(new Version(12, 2, 2));

        services.AddDbContext<ShaGeneratorDbContext>(options =>
            options.UseMySql(connectionString, serverVersion));

        services.AddScoped<IShaGeneratorDbContext>(sp => sp.GetRequiredService<ShaGeneratorDbContext>());

        return services;
    }

    private static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqOptions>(configuration.GetSection(RabbitMqOptions.SectionName));
        services.AddScoped<IHashPublisher, RabbitMqHashPublisher>();
        return services;
    }
}
