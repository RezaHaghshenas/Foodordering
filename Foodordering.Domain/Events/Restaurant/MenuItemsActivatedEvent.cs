using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Events.Restaurant
{
    public class MenuItemsActivatedEvent : DomainEvent
    {
        public Guid RestaurantId { get; set; }

        public string MenuItemName { get; set; }

        public MenuItemsActivatedEvent(Guid id, string name)
        {
            RestaurantId = id;
            MenuItemName = name;
        }
    }
}
