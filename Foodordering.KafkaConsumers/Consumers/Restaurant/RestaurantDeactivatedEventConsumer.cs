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
    public class RestaurantDeactivatedEventConsumer : KafkaConsumerBase<RestaurantDeactivatedEvent>
    {
        public RestaurantDeactivatedEventConsumer(IConsumer<string, string> consumer, ILogger<RestaurantDeactivatedEventConsumer> logger)
            : base(consumer, logger) { }

        protected override string TopicName => "RestaurantDeactivatedEvent";

        protected override async Task HandleEventAsync(RestaurantDeactivatedEvent @event)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"🔴 Restaurant deactivated: {@event.RestaurantId}");
            });
        }
    }
}
