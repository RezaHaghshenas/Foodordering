using Foodordering.Domain.ValueObjects.FoodOrderingSystem.Domain.ValueObjects.Enums;
using FoodOrderingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid RestaurantId { get; private set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public decimal TotalPrice { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public string DeliveryAddress { get; private set; } = string.Empty;
        public string CustomerPhone { get; private set; } = string.Empty;
        public PaymentMethod PaymentMethod { get; private set; } = PaymentMethod.Cash;
        public DeliveryMethod DeliveryMethod { get; private set; } = DeliveryMethod.Delivery;
        public bool IsPaid { get; private set; }

        public List<OrderItem> Items { get; private set; } = new();
        public List<Payment> Payments { get; private set; } = new();

        public List<OrderReview> orderReviews { get; private set; } = new();    

        public Restaurant Restaurant { get; private set; } = null!;
        public User User { get; private set; } = null!;

        private Order() { }

        public Order(Guid userId, Guid restaurantId, string address, string phone)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            RestaurantId = restaurantId;
            DeliveryAddress = address;
            CustomerPhone = phone;
        }

        public void AddItem(Guid menuItemId, int quantity, decimal price)
        {
            if (quantity <= 0) throw new InvalidOperationException("Invalid quantity");
            Items.Add(new OrderItem(Id, menuItemId, quantity, price));

            RecalculateTotal();
        }

        public void MarkAsPaid() => IsPaid = true;

        public void ChangeStatus(OrderStatus status)
        {
            if (status == OrderStatus.Delivered && !IsPaid)
                throw new InvalidOperationException("Cannot mark as delivered before payment");
            Status = status;
        }

        private void RecalculateTotal()
        {
            TotalPrice = Items.Sum(i => i.Price * i.Quantity);
        }
    }
}
