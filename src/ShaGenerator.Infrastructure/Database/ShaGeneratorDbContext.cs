using Microsoft.EntityFrameworkCore;

namespace ShaGenerator.Infrastructure.Database;

public class ShaGeneratorDbContext : DbContext
{
    public ShaGeneratorDbContext(DbContextOptions<ShaGeneratorDbContext> options)
        : base(options)
    {
    }
}
