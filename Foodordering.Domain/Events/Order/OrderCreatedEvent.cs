using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Events.Order
{
    public class OrderCreatedEvent : DomainEvent
    {
        public Guid OrdertId { get; set; }

        public string OwnerName { get; set; }
        public string OwnerFamily { get; set; }


        public string OwnerNationalCode { get; set; }


        public OrderCreatedEvent(Guid resturantid, string ownername, string ownerfamily, string ownernationalcode)
        {
            OrdertId = resturantid;
            OwnerName = ownername;
            OwnerFamily = ownerfamily;
            OwnerNationalCode = ownernationalcode;


        }
    }
}
