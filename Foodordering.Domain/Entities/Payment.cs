using Foodordering.Domain.Entities;
using Foodordering.Domain.ValueObjects.FoodOrderingSystem.Domain.ValueObjects.Enums;
using System;

namespace FoodOrderingSystem.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal Amount { get; private set; }
        public PaymentMethod Method { get; private set; }
        public PaymentStatus Status { get; private set; } = PaymentStatus.Pending;
        public string? GatewayTransactionId { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? PaidAt { get; private set; }

        public Order Order { get; private set; } = null!;

        private Payment() { }

        public Payment(Guid orderId, decimal amount, PaymentMethod method)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            Amount = amount;
            Method = method;
        }

        public void MarkSuccess(string gatewayTransactionId)
        {
            Status = PaymentStatus.Success;
            GatewayTransactionId = gatewayTransactionId;
            PaidAt = DateTime.UtcNow;
        }

        public void MarkFailed() => Status = PaymentStatus.Failed;
        public void Refund() => Status = PaymentStatus.Refunded;
    }
}
