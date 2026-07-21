namespace ShaGenerator.Domain.Hashes;

public class Hash
{
    public Guid Id { get; }
    public DateOnly Date { get; } = new DateOnly(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
    public required string Sha1 { get; set; }
}
