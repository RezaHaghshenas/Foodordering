using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Events.Restaurant
{
        public class RestaurantDeactivatedEvent : DomainEvent
        {
            public Guid RestaurantId { get; set; }

            public RestaurantDeactivatedEvent(Guid restaurantId)
            {
                RestaurantId = restaurantId;
            }
        }
    
}
