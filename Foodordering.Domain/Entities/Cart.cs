using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid UserId { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedAt { get; private set; }

        public List<CartItem> Items { get; private set; } = new();

        private Cart() { }

        public Cart(Guid userId)
        {
            UserId = userId;
        }

        public void AddItem(Guid menuItemId, int quantity, decimal price , decimal eachprice)
        {
            var existing = Items.FirstOrDefault(i => i.MenuItemId == menuItemId);
            if (existing != null)
                existing.IncreaseQuantity(quantity);
            else
                Items.Add(new CartItem(menuItemId, quantity, price , eachprice));

            LastUpdatedAt = DateTime.UtcNow;
        }

        public void RemoveMenuItem(Guid menuItemId)
        {
            Items.RemoveAll(i => i.MenuItemId == menuItemId);
            LastUpdatedAt = DateTime.UtcNow;
        }

        public void RemoveCartItem(CartItem item)
        {
            Items.Remove(item);
            LastUpdatedAt = DateTime.UtcNow;
        }

        public void Clear()
        {
            Items.Clear();
            LastUpdatedAt = DateTime.UtcNow;
        }


        public void MarkForDeletion()
        {
            // در صورت نیاز می‌تونی رویداد دامنه‌ای اضافه کنی یا فلگ بزنی
            Items.Clear(); // پاک‌سازی آیتم‌ها
                           // می‌تونی در آینده فلگ IsDeleted اضافه کنی
        }

        public Order ToOrder(Guid restaurantId, string address, string phone)
        {
            var order = new Order(UserId, restaurantId, address, phone);
            foreach (var item in Items)
            {
                order.AddItem(item.MenuItemId, item.Quantity, item.Price);
            }
            return order;
        }
    }
}
