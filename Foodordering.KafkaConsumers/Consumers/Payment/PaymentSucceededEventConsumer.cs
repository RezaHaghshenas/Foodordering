using Confluent.Kafka;
using Foodordering.Domain.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.KafkaConsumers.Consumers.Payment
{
    public class PaymentSucceededEventConsumer : KafkaConsumerBase<PaymentSucceededEvent>
    {
        public PaymentSucceededEventConsumer(IConsumer<string, string> consumer, ILogger<PaymentSucceededEventConsumer> logger)
            : base(consumer, logger)
        {
        }

        protected override string TopicName => "PaymentSucceededEvent";

        protected override async Task HandleEventAsync(PaymentSucceededEvent @event)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"💰 Payment Success: {@event.PaymentId} - Amount: {@event.Amount} - Tx: {@event.GatewayTransactionId}");
                // اینجا می‌تونی مثلاً ایمیل بزنی، نوتیفیکیشن بدی، یا سفارش رو به سیستم ارسال غذا بفرستی
            });
        }
    }
}
