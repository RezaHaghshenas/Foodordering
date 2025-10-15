using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Events.Payment
{
    public class PaymentRefundedEvent : DomainEvent
    {
        public Guid PaymentId { get; }
        public Guid OrderId { get; }
        public decimal Amount { get; }
        public string Reason { get; }

        public PaymentRefundedEvent(Guid paymentId, Guid orderId, decimal amount, string reason)
        {
            PaymentId = paymentId;
            OrderId = orderId;
            Amount = amount;
            Reason = reason;
        }
    }
}
