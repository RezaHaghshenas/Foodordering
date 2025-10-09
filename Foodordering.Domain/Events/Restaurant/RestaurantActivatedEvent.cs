using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Events.Restaurant
{

        public class RestaurantActivatedEvent : DomainEvent
        {
            public Guid RestaurantId { get; set; }

            public RestaurantActivatedEvent(Guid restaurantId)
            {
                RestaurantId = restaurantId;
            }
        }
    
}
