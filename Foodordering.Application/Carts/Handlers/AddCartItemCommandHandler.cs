using Foodordering.Application.Carts.Command;
using Foodordering.Application.Common.Interfaces;
using Foodordering.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Common.Carts.Handlers
{
    public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand>
    {
        private readonly IAppDbContext _context;

        public AddCartItemCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            var cart = await _context.carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);

            var MenuItem = await _context.menuItems.FirstOrDefaultAsync(u=> u.Id == request.MenuItemId);

            if (MenuItem == null)
            {
                throw new Exception("چنین آیتمی در منو هیچ رستورانی یافت نشد "); 
            }


            if (cart == null)
            {
                cart = new Cart(request.UserId);
                _context.carts.Add(cart);
            }

            cart.AddItem(request.MenuItemId, request.Quantity, (MenuItem.Price * request.Quantity) ,MenuItem.Price);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
