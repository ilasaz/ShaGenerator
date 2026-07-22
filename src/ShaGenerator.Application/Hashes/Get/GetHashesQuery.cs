using Application.Abstractions.Messaging;
using ShaGenerator.Contracts.Hashes;

namespace ShaGenerator.Application.Hashes.Get;

public sealed record GetHashesQuery() : IQuery<HashesByDateResponse>;
