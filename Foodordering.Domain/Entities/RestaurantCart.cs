using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Entities
{
    public class RestaurantCart
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid RestaurantId { get; private set; }
        public List<CartItem> Items { get; private set; } = new();
        public decimal DiscountAmount { get; private set; } = 0;
        public decimal DeliveryFee { get; private set; } = 0;

        public Guid CartId { get; set; }
        public Cart Cart { get; set; } = null!;

        public RestaurantCart(Guid restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public void AddItem(Guid menuItemId, int quantity, decimal price, decimal eachPrice)
        {
            var existing = Items.FirstOrDefault(i => i.MenuItemId == menuItemId);
            if (existing != null)
            {
                existing.IncreaseQuantity(quantity);
                existing.EachPrice = eachPrice;
            }
            else
            {
                Items.Add(new CartItem(menuItemId, quantity, price, eachPrice, Id));
            }
        }

        public void RemoveItem(Guid menuItemId)
        {
            Items.RemoveAll(i => i.MenuItemId == menuItemId);
        }

        public void ApplyDiscount(decimal amount)
        {
            var total = Items.Sum(i => i.Price);
            if (amount < 0 || amount > total)
                throw new InvalidOperationException("مقدار تخفیف نامعتبر است");

            DiscountAmount = amount;
        }

        public void SetDeliveryFee(decimal fee)
        {
            if (fee < 0)
                throw new InvalidOperationException("هزینه ارسال نامعتبر است");

            DeliveryFee = fee;
        }

        public Order ToOrder(Guid userId, Address address, string phone)
        {
            var order = new Order(userId, RestaurantId, address, phone);
            foreach (var item in Items)
            {
                order.AddItem(item.MenuItemId, item.Quantity, item.Price);
            }
            order.ApplyDiscount(DiscountAmount);
            order.SetDeliveryFee(DeliveryFee);
            return order;
        }
    }
}
