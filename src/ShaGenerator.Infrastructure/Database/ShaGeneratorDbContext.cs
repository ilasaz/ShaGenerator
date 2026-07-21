using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ShaGenerator.Domain.Hashes;

namespace ShaGenerator.Infrastructure.Database;

public class ShaGeneratorDbContext : DbContext
{
    public DbSet<Hash> Hashes { get; set; }


    public ShaGeneratorDbContext(DbContextOptions<ShaGeneratorDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
