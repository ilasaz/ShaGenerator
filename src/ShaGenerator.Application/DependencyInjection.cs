using Application.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;
using ShaGenerator.Application.Hashes.Get;
using ShaGenerator.Contracts.Hashes;

namespace ShaGenerator.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IQueryHandler<GetHashesQuery, HashesByDateResponse>, GetHashesQueryHandler>();

        return services;
    }
}
