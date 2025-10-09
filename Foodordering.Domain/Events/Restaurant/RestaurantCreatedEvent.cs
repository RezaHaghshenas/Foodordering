using Foodordering.Domain.Entities;
using Foodordering.Domain.Events.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Events.Restaurant
{
    public class RestaurantCreatedEvent : DomainEvent
    {
        public Guid RestaurantId { get; set; }

        public string OwnerName { get; set; }
        public string OwnerFamily { get; set; }


        public string OwnerNationalCode { get; set; }


        public RestaurantCreatedEvent(Guid resturantid , string ownername , string ownerfamily , string ownernationalcode) 
        {
            RestaurantId = resturantid;
            OwnerName = ownername;  
            OwnerFamily = ownerfamily;
            OwnerNationalCode = ownernationalcode;

                
        } 
    }
}

