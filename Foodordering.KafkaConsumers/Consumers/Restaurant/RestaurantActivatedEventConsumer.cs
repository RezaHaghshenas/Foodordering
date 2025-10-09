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
    public class RestaurantActivatedEventConsumer : KafkaConsumerBase<RestaurantActivatedEvent>
    {
        public RestaurantActivatedEventConsumer(IConsumer<string, string> consumer, ILogger<RestaurantActivatedEventConsumer> logger)
            : base(consumer, logger) { }

        protected override string TopicName => "RestaurantActivatedEvent";

        protected override async Task HandleEventAsync(RestaurantActivatedEvent @event)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"🟢 Restaurant activated: {@event.RestaurantId}");
            });
        }
    }
}
