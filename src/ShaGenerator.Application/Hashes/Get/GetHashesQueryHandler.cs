using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using ShaGenerator.Application.Abstractions.Data;
using ShaGenerator.Contracts.Hashes;

namespace ShaGenerator.Application.Hashes.Get;

internal sealed class GetHashesQueryHandler(IShaGeneratorDbContext context)
    : IQueryHandler<GetHashesQuery, HashesByDateResponse>
{
    public async Task<HashesByDateResponse> Handle(GetHashesQuery query, CancellationToken cancellationToken)
    {
        List<HashDto> hashes = await context.Hashes
            .AsNoTracking()
            .GroupBy(h => h.Date)
            .OrderBy(g => g.Key)
            .Select(g => new HashDto
            {
                Date = g.Key.ToString("yyyy-MM-dd"),
                Count = g.LongCount()
            })
            .ToListAsync(cancellationToken);

        var result = new HashesByDateResponse { Hashes = hashes };

        return result;
    }

}
