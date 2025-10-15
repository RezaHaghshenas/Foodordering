using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Foodordering.Domain.Entities
{
    
        public class Cart
        {
            public Guid Id { get; private set; } = Guid.NewGuid();
            public Guid UserId { get; private set; }
            public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
            public DateTime? LastUpdatedAt { get; private set; }

            public Decimal TotalPrice_AfterDiscount { get; set; }

            public Guid DiscountCode_Id { get; set; }

        public List<RestaurantCart> RestaurantCarts { get; private set; } = new();

            private Cart() { }

            public Cart(Guid userId)
            {
                UserId = userId;
            }

            public void AddItem(Guid restaurantId, Guid menuItemId, int quantity,decimal eachPrice)
            {
                var restaurantCart = RestaurantCarts.FirstOrDefault(rc => rc.RestaurantId == restaurantId);
                if (restaurantCart == null)
                {
                    restaurantCart = new RestaurantCart(restaurantId);
                    RestaurantCarts.Add(restaurantCart);
                }
            var totalPrice = eachPrice * quantity;
            restaurantCart.AddItem(menuItemId, quantity, totalPrice, eachPrice);
                LastUpdatedAt = DateTime.UtcNow;
            }





            public void RemoveRestaurant(Guid restaurantId)
            {
                RestaurantCarts.RemoveAll(rc => rc.RestaurantId == restaurantId);
                LastUpdatedAt = DateTime.UtcNow;
            }

            public void Clear()
            {
                RestaurantCarts.Clear();
                LastUpdatedAt = DateTime.UtcNow;
            }

            public List<Order> ToOrders(Address address, string phone)
            {
                var orders = new List<Order>();
                foreach (var rc in RestaurantCarts)
                {
                    var order = rc.ToOrder(UserId, address, phone);
                    orders.Add(order);
                }
                return orders;
            }
        }
    }


