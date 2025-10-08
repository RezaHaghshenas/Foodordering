using Confluent.Kafka;
using Foodordering.Domain.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.KafkaConsumers.Consumers
{
    public class UserPasswordResetEventConsumer
  : KafkaConsumerBase<UserPasswordResetEvent>
    {
        public UserPasswordResetEventConsumer(IConsumer<string, string> consumer, ILogger<UserPasswordResetEventConsumer> logger)
            : base(consumer, logger) { }

        protected override string TopicName => "UserRegisteredEvent";

        protected override async Task HandleEventAsync(UserPasswordResetEvent @event)
        {
            // اینجا مثلا ایمیل خوشامد ارسال کن
            await Task.Run(() =>
            {
                Console.WriteLine($"👋 Welcome {@event.UserId}");
            });
        }
    }
}
