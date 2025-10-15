using Foodordering.Domain.ValueObjects.FoodOrderingSystem.Domain.ValueObjects.Enums;
using FoodOrdering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Foodordering.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid RestaurantId { get; private set; }

        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public decimal TotalPrice { get; private set; }
        public decimal DeliveryFee { get; private set; } = 0;
        public decimal DiscountAmount { get; private set; } = 0;
        public string? DiscountCode { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public Address DeliveryAddress { get; private set; }
        public string CustomerPhone { get; private set; } = string.Empty;

        public PaymentMethod PaymentMethod { get; private set; } = PaymentMethod.Cash;
        public DeliveryMethod DeliveryMethod { get; private set; } = DeliveryMethod.Delivery;
        public bool IsPaid { get; private set; }

        public List<OrderItem> Items { get; private set; } = new();
        public List<Payment> Payments { get; private set; } = new();
        public List<OrderReview> OrderReviews { get; private set; } = new();

        public Restaurant Restaurant { get; private set; } = null!;
        public User User { get; private set; } = null!;

        private Order() { }

        public Order(Guid userId, Guid restaurantId, Address address, string phone)
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

        public void ApplyDiscount(decimal amount, string? code = null)
        {
            if (amount < 0) throw new InvalidOperationException("Invalid discount amount");
            DiscountAmount = amount;
            DiscountCode = code;
            RecalculateTotal();
        }

        public void SetDeliveryFee(decimal fee)
        {
            if (fee < 0) throw new InvalidOperationException("Invalid delivery fee");
            DeliveryFee = fee;
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
            var itemsTotal = Items.Sum(i => i.Price * i.Quantity);
            TotalPrice = itemsTotal + DeliveryFee - DiscountAmount;
        }
    }
}
