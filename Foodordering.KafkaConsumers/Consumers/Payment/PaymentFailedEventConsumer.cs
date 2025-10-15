using Confluent.Kafka;
using Foodordering.Domain.Events.Payment;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.KafkaConsumers.Consumers.Payment
{
    public class PaymentFailedEventConsumer : KafkaConsumerBase<PaymentFailedEvent>
    {
        public PaymentFailedEventConsumer(IConsumer<string, string> consumer, ILogger<PaymentFailedEventConsumer> logger)
            : base(consumer, logger)
        {
        }

        protected override string TopicName => "PaymentFailedEvent";

        protected override async Task HandleEventAsync(PaymentFailedEvent @event)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"❌ Payment Failed: {@event.PaymentId} - Amount: {@event.Amount}");
                // اینجا می‌تونی نوتیفیکیشن بزنی یا لاگ‌گیری کنی
            });
        }
    }

}
