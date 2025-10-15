using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid MenuItemId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public decimal EachPrice { get; set; }

        public string ?Note { get; private set; }

        

        public Guid RestaurantCartId { get; set; }
        public RestaurantCart RestaurantCart { get; set; } = null!;

        private CartItem() { }

        public CartItem(Guid menuItemId, int quantity, decimal price, decimal eachPrice, Guid restaurantCartId)
        {
            MenuItemId = menuItemId;
            Quantity = quantity;
            Price = price;
            EachPrice = eachPrice;
            RestaurantCartId = restaurantCartId;
        }

        public void IncreaseQuantity(int increaseValue)
        {
            Quantity += increaseValue;
            Price = EachPrice * Quantity;
        }

        public void UpdateQuantity(int newQuantity)
        {
            Quantity = newQuantity;
            Price = EachPrice * newQuantity;
        }


        public void AddNote(string note)
        {
            Note = note; 
        }
    }
}