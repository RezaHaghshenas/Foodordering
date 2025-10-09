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
    public class MenuItemsActivatedEventConsumer : KafkaConsumerBase<MenuItemsActivatedEvent>
    {
        public MenuItemsActivatedEventConsumer(IConsumer<string, string> consumer, ILogger<MenuItemsActivatedEventConsumer> logger)
            : base(consumer, logger) { }

        protected override string TopicName => "MenuItemsActivatedEvent";

        protected override async Task HandleEventAsync(MenuItemsActivatedEvent @event)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"📋 Menu item َctivated: {@event.MenuItemName} to restaurant {@event.RestaurantId}");
            });
        }
    }
}
