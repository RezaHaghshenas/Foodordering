using FoodOrderingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid MenuItemId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        public Order Order { get; private set; } = null!;
        public MenuItem MenuItem { get; private set; } = null!;

        private OrderItem() { }

        public OrderItem(Guid orderId, Guid menuItemId, int quantity, decimal price)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            MenuItemId = menuItemId;
            Quantity = quantity;
            Price = price;
        }
    }
}
