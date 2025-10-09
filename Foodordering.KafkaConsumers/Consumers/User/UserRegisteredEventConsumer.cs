using Confluent.Kafka;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Foodordering.Domain.Events.User;

namespace Foodordering.KafkaConsumers.Consumers.User
{
    public class UserRegisteredEventConsumer
     : KafkaConsumerBase<UserRegisteredEvent>
    {
        public UserRegisteredEventConsumer(IConsumer<string, string> consumer, ILogger<UserRegisteredEventConsumer> logger)
            : base(consumer, logger) { }

        protected override string TopicName => "UserRegisteredEvent";

        protected override async Task HandleEventAsync(UserRegisteredEvent @event)
        {
            // اینجا مثلا ایمیل خوشامد ارسال کن
            await Task.Run(() =>
            {
                Console.WriteLine($"👋 Welcome {@event.Email}");
            });
        }
    }


}