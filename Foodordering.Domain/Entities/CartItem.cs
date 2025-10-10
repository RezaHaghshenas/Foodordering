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

        private CartItem() { }

        public CartItem(Guid menuItemId, int quantity, decimal price , decimal eachprice)
        {
            MenuItemId = menuItemId;
            Quantity = quantity;
            Price = price;
            EachPrice = eachprice;
        }

        public void IncreaseQuantity(int IncreaseValue)
        {
            Quantity += IncreaseValue;
            Price = (EachPrice * Quantity);
        }

        public void UpdateQuantity(int newQuantity)
            {
            Quantity = newQuantity;
            Price = (EachPrice * newQuantity);        
        }
    }
}
