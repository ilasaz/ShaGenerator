using System;
using System.Collections.Generic;
using System.Text;
using ShaGenerator.Domain.Hashes;

namespace ShaGenerator.Application.Hashes;

public interface IHashPublisher
{
    ValueTask PublishAsync(string message, CancellationToken cancellationToken = default);
}
