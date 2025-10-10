using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Carts.Command
{
    public class AddCartItemCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid MenuItemId { get; set; }
        public int Quantity { get; set; }

        public AddCartItemCommand(Guid userId, Guid menuItemId, int quantity)
        {
            UserId = userId;
            MenuItemId = menuItemId;
            Quantity = quantity;
        }
    }
}
