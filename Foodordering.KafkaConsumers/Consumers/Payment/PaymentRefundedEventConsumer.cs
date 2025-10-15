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
    public class PaymentRefundedEventConsumer : KafkaConsumerBase<PaymentRefundedEvent>
    {
        public PaymentRefundedEventConsumer(IConsumer<string, string> consumer, ILogger<PaymentRefundedEventConsumer> logger)
            : base(consumer, logger)
        {
        }

        protected override string TopicName => "PaymentRefundedEvent";

        protected override async Task HandleEventAsync(PaymentRefundedEvent @event)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"🔁 Payment Refunded: {@event.PaymentId} - Amount: {@event.Amount} - Reason: {@event.Reason}");
                // اینجا می‌تونی ایمیل بزنی، نوتیفیکیشن بدی، یا لاگ‌گیری کنی
            });
        }
    }

}
