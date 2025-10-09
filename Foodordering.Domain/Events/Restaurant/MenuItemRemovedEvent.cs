using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Events.Restaurant
{
    public class MenuItemRemovedEvent : DomainEvent
    {
        public Guid RestaurantId { get; set; }

        public string Name { get; set; }

        public MenuItemRemovedEvent(Guid id , string name)
        {
            RestaurantId = id;
            Name = name;        
        }
    }
}
