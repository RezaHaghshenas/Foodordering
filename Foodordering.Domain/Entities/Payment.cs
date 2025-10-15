using Foodordering.Domain.Entities;
using Foodordering.Domain.Events;
using Foodordering.Domain.Events.Order;
using Foodordering.Domain.Events.Payment;
using Foodordering.Domain.ValueObjects.FoodOrderingSystem.Domain.ValueObjects.Enums;
using System;

namespace FoodOrdering.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid CartId { get; set; }
        public decimal Amount { get; private set; }
        public PaymentMethod Method { get; private set; }
        public PaymentStatus Status { get; private set; } = PaymentStatus.Pending;
        public string? GatewayTransactionId { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? PaidAt { get; private set; }

        public Order Order { get; private set; } = null!;

        public Cart cart { get; private set; } = null!;

        public List<DomainEvent> DomainEvents { get; private set; } = new List<DomainEvent>();

        private Payment() { }

        public Payment(Guid cartid, decimal amount, PaymentMethod method)
        {
            Id = Guid.NewGuid();
            CartId = cartid; 
            Amount = amount;
            Method = method;
            CreatedAt = DateTime.UtcNow;    
        }

        public void MarkSuccess(string gatewayTransactionId)
        {
            Status = PaymentStatus.Success;
            GatewayTransactionId = gatewayTransactionId;
            PaidAt = DateTime.UtcNow;

            DomainEvents.Add(new Foodordering.Domain.Events.PaymentSucceededEvent(Id, OrderId, Amount, gatewayTransactionId));
        }

        public void MarkFailed()
        {
            Status = PaymentStatus.Failed;
            DomainEvents.Add(new Foodordering.Domain.Events.Payment.PaymentFailedEvent(Id, OrderId, Amount));
        }

        public void Refund(string reason)
        {
            if (Status != PaymentStatus.Success)
                throw new InvalidOperationException("فقط پرداخت‌های موفق قابل بازپرداخت هستند.");

            Status = PaymentStatus.Refunded;
            DomainEvents.Add(new Foodordering.Domain.Events.Payment.PaymentRefundedEvent(Id, OrderId, Amount, reason));
        }

    }
}
