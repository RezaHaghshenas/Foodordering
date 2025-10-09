using Confluent.Kafka;
using Foodordering.Domain.Events.Restaurant;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.KafkaConsumers.Consumers.Restaurant
{
    public class RestaurantCreatedEventConsumer : KafkaConsumerBase<RestaurantCreatedEvent>
    {
        public RestaurantCreatedEventConsumer(IConsumer<string, string> consumer, ILogger<RestaurantCreatedEventConsumer> logger)
            : base(consumer, logger)
        {
        }

        protected override string TopicName => "RestaurantCreatedEvent";

        protected override async Task HandleEventAsync(RestaurantCreatedEvent @event)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"🏪 Restaurant Created: {@event.OwnerName} {@event.OwnerFamily} - {@event.RestaurantId}");
            });
        }
    }

}
