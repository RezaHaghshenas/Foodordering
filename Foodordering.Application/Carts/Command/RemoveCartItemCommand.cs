using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Carts.Command
{
    public class RemoveCartItemCommand : IRequest<bool>
    {

        public Guid CartId { get; set; }

        public Guid CartItemId { get; set; }


        public RemoveCartItemCommand(Guid cartId, Guid cartItemId)
        {
            CartId = cartId;
            CartItemId = cartItemId;
        }       
    }
}
