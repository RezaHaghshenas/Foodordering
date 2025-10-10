using Foodordering.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Carts.Command
{
    public class ConvertCartToOrderCommand : IRequest<Guid>
    {
        public Guid CartId { get; set; }
        public Guid RestaurantId { get; set; }
        public Address DeliveryAddress { get; set; } 
        public string CustomerPhone { get; set; } = string.Empty;

        public ConvertCartToOrderCommand(Guid cartId, Guid restaurantId, Address deliveryAddress, string customerPhone)
        {
            CartId = cartId;
            RestaurantId = restaurantId;
            DeliveryAddress = deliveryAddress;
            CustomerPhone = customerPhone;
        }
    }

}
