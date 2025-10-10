using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Carts.Command
{
    public class UpdateCartItemQuantityCommand : IRequest<bool>
    {
        public Guid CartId { get; set; }
        public Guid CartItemId { get; set; }
        public int NewQuantity { get; set; }

        public UpdateCartItemQuantityCommand(Guid cartId, Guid cartItemId, int newQuantity)
        {
            CartId = cartId;
            CartItemId = cartItemId;
            NewQuantity = newQuantity;
        }
    }

}
