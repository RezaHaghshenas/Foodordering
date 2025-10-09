using Confluent.Kafka;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Foodordering.Domain.Events.User;

namespace Foodordering.KafkaConsumers.Consumers.User
{
    
     public class DeactivateUserEventConsumer
     : KafkaConsumerBase<UserDeactivatedEvent>
    {
        public DeactivateUserEventConsumer(IConsumer<string, string> consumer, ILogger<DeactivateUserEventConsumer> logger)
            : base(consumer, logger) { }

        protected override string TopicName => "UserDeactivatedEvent";

        protected override async Task HandleEventAsync(UserDeactivatedEvent @event)
        {
            // اینجا مثلا ایمیل خوشامد ارسال کن
            await Task.Run(() =>
            {
                Console.WriteLine($"👋 Welcome {@event.UserId}");
            });
        }
    }
}




