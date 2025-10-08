using Confluent.Kafka;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Foodordering.Domain.Events;
public abstract class KafkaConsumerBase<TEvent> : IHostedService
    where TEvent : DomainEvent
{
    private readonly IConsumer<string, string> _consumer;
    private readonly ILogger _logger;

    protected KafkaConsumerBase(IConsumer<string, string> consumer, ILogger logger)
    {
        _consumer = consumer;
        _logger = logger;
    }

    protected abstract string TopicName { get; }
    protected abstract Task HandleEventAsync(TEvent @event);

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _consumer.Subscribe(TopicName);
        Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(cancellationToken);
                    var @event = JsonSerializer.Deserialize<TEvent>(result.Message.Value);
                    if (@event != null)
                        await HandleEventAsync(@event);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Kafka consume error on {TopicName}", TopicName);
                }
            }
        }, cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _consumer.Close();
        _logger.LogInformation("Kafka consumer for {TopicName} stopped.", TopicName);
        return Task.CompletedTask;
    }
}
