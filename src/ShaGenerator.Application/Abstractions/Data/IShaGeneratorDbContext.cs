using Microsoft.EntityFrameworkCore;
using ShaGenerator.Domain.Hashes;

namespace ShaGenerator.Application.Abstractions.Data;

public interface IShaGeneratorDbContext
{
    DbSet<Hash> Hashes { get; }
}
