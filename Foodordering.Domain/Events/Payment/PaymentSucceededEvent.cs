using Foodordering.Domain.Entities;
using System;

namespace Foodordering.Domain.Events
{
    public class PaymentSucceededEvent : DomainEvent
    {
        public Guid PaymentId { get; }
        public Guid OrderId { get; }
        public decimal Amount { get; }
        public string GatewayTransactionId { get; }

        public PaymentSucceededEvent(Guid paymentId, Guid orderId, decimal amount, string gatewayTransactionId)
        {
            PaymentId = paymentId;
            OrderId = orderId;
            Amount = amount;
            GatewayTransactionId = gatewayTransactionId;
        }
    }
}
