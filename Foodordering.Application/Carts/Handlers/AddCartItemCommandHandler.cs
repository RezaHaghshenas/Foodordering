using Foodordering.Application.Carts.Command;
using Foodordering.Application.Common.Interfaces;
using Foodordering.Domain.Entities;
using FoodOrderingSystem.Domain.Entities;
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
                .Include(c => c.RestaurantCarts)
                .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);

            var menuItem = await _context.menuItems
      .FirstOrDefaultAsync(u => u.Id == request.MenuItemId && u.RestaurantId == request.RestaurantId, cancellationToken);

            if (menuItem == null)
                throw new Exception("آیتم منو برای این رستوران یافت نشد.");



            if (cart == null)
            {
                cart = new Cart(request.UserId);
                _context.carts.Add(cart);
            }
            cart.AddItem(request.RestaurantId, request.MenuItemId, request.Quantity,  menuItem.Price);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
