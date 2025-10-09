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
    public class MenuItemRemovedEventConsumer : KafkaConsumerBase<MenuItemRemovedEvent>
    {
        public MenuItemRemovedEventConsumer(IConsumer<string, string> consumer, ILogger<MenuItemRemovedEventConsumer> logger)
            : base(consumer, logger) { }

        protected override string TopicName => "MenuItemRemovedEvent";

        protected override async Task HandleEventAsync(MenuItemRemovedEvent @event)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"📋 Menu item Removed: {@event.Name} to restaurant {@event.RestaurantId}");
            });
        }
    }
}



