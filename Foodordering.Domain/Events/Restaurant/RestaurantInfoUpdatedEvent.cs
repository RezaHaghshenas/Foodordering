using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Events.Restaurant
{
        public class RestaurantInfoUpdatedEvent : DomainEvent
        {
            public Guid RestaurantId { get; set; }
            public string NewName { get; set; }
            public string NewDescription { get; set; }
            public string NewPhone { get; set; }

            public RestaurantInfoUpdatedEvent(Guid restaurantId, string newName, string newDescription, string newPhone)
            {
                RestaurantId = restaurantId;
                NewName = newName;
                NewDescription = newDescription;
                NewPhone = newPhone;
            }
        }
    
}
