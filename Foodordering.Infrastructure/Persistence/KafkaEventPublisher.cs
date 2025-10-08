using Foodordering.Application.Common.Interfaces;
using Foodordering.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Confluent.Kafka;

namespace Foodordering.Infrastructure.Persistence
{
    public class KafkaEventPublisher : IEventPublisher
    {
        private readonly IProducer<string, string> _producer;

        public KafkaEventPublisher(IProducer<string, string> producer)
        {
            _producer = producer;
        }

        public async Task PublishAsync(DomainEvent domainEvent)
        {
            var topic = domainEvent.GetType().Name;
            var json = JsonSerializer.Serialize(domainEvent);

            await _producer.ProduceAsync(topic, new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = json
            });
        }
    }
}

