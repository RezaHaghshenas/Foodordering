using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foodordering.Domain.Events.Restaurant;

namespace Foodordering.KafkaConsumers.Consumers.Restaurant
{
    public class MenuItemAddedEventConsumer : KafkaConsumerBase<MenuItemAddedEvent>
    {
        public MenuItemAddedEventConsumer(IConsumer<string, string> consumer, ILogger<MenuItemAddedEventConsumer> logger)
            : base(consumer, logger) { }

        protected override string TopicName => "MenuItemAddedEvent";

        protected override async Task HandleEventAsync(MenuItemAddedEvent @event)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"📋 Menu item added: {@event.MenuItemName} to restaurant {@event.RestaurantId}");
            });
        }
    }
}
