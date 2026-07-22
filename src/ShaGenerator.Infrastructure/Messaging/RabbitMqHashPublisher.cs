using System.Text;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using ShaGenerator.Application.Hashes;

namespace ShaGenerator.Infrastructure.Messaging;

/// <summary>
/// Lazily creates a single connection/channel per scope and publishes messages
/// to the configured queue. Registered as Scoped so the connection lives for
/// the duration of a single HTTP request (i.e. one batch of 40,000).
/// </summary>
public sealed class RabbitMqHashPublisher : IHashPublisher, IAsyncDisposable
{
    private readonly RabbitMqOptions _options;
    private IConnection? _connection;
    private IChannel? _channel;
    private bool _initialized;

    public RabbitMqHashPublisher(IOptions<RabbitMqOptions> options)
        => _options = options.Value;

    public async ValueTask PublishAsync(string message, CancellationToken cancellationToken = default)
    {
        await EnsureInitializedAsync(cancellationToken);

        byte[] body = Encoding.UTF8.GetBytes(message);

        await _channel!.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: _options.QueueName,
            mandatory: false,
            body: body,
            cancellationToken: cancellationToken);
    }

    private async Task EnsureInitializedAsync(CancellationToken cancellationToken)
    {
        if (_initialized)
        {
            return;
        }

        var factory = new ConnectionFactory
        {
            HostName = _options.HostName,
            Port = _options.Port,
            UserName = _options.UserName,
            Password = _options.Password,
        };

        _connection = await factory.CreateConnectionAsync(cancellationToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await _channel.QueueDeclareAsync(
            queue: _options.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);

        _initialized = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (_channel is not null)
        {
            await _channel.DisposeAsync();
        }

        if (_connection is not null)
        {
            await _connection.DisposeAsync();
        }
    }
}
