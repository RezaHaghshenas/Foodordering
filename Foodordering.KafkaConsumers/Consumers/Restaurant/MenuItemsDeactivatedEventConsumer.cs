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
    public class MenuItemsDeactivatedEventConsumer : KafkaConsumerBase<MenuItemsDeactivatedEvent>
    {
        public MenuItemsDeactivatedEventConsumer(IConsumer<string, string> consumer, ILogger<MenuItemsDeactivatedEventConsumer> logger)
            : base(consumer, logger) { }

        protected override string TopicName => "MenuItemsDeactivatedEvent";

        protected override async Task HandleEventAsync(MenuItemsDeactivatedEvent @event)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"📋 Menu item Deactivated: {@event.MenuItemName} to restaurant {@event.RestaurantId}");
            });
        }
    }

}
