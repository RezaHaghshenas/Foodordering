using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Events.Restaurant
{
    public class MenuItemAddedEvent : DomainEvent
    {
        public Guid RestaurantId { get; set; }
        public Guid MenuItemId { get; set; }
        public string MenuItemName { get; set; }

        public MenuItemAddedEvent(Guid restaurantId, Guid menuItemId, string menuItemName)
        {
            RestaurantId = restaurantId;
            MenuItemId = menuItemId;
            MenuItemName = menuItemName;
        }
    }

}
