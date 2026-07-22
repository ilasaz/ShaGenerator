using System.Security.Cryptography;
using ShaGenerator.Application.Hashes;

namespace ShaGenerator.Application.Services;

public sealed class HashGeneratorService
{
    private const int HashCount = 40_000;
    private const int RandomByteLength = 32;

    private readonly IHashPublisher _publisher;

    public HashGeneratorService(IHashPublisher publisher) => _publisher = publisher;

    public async Task<int> GenerateAndPublishAsync(CancellationToken cancellationToken = default)
    {
        byte[] buffer = new byte[RandomByteLength];

        for (int i = 0; i < HashCount; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            RandomNumberGenerator.Fill(buffer);
            byte[] hashBytes = SHA1.HashData(buffer);
            string sha1 = Convert.ToHexStringLower(hashBytes);

            await _publisher.PublishAsync(sha1, cancellationToken);
        }

        return HashCount;
    }
}
