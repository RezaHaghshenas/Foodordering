using Foodordering.Application.Carts.Command;
using Foodordering.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Carts.Handlers
{
    public class RemoveCartItemCommandHandler : IRequestHandler<RemoveCartItemCommand, bool>    
    {
        private readonly IAppDbContext _context ;
        public RemoveCartItemCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
        {
            var cart = await _context.carts
                .Include(c => c.RestaurantCarts)
                    .ThenInclude(rc => rc.Items)
                .FirstOrDefaultAsync(c => c.Id == request.CartId, cancellationToken);

            if (cart == null)
                throw new InvalidOperationException("سبد خرید یافت نشد");

            var restaurantCart = cart.RestaurantCarts
                .FirstOrDefault(rc => rc.RestaurantId == request.RestaurantId);

            if (restaurantCart == null)
                throw new InvalidOperationException("سبد مربوط به این رستوران یافت نشد");

            var item = restaurantCart.Items
                .FirstOrDefault(i => i.Id == request.CartItemId);

            if (item == null)
                throw new InvalidOperationException("آیتم مورد نظر یافت نشد");

            restaurantCart.RemoveItem(item.Id);

            if (!restaurantCart.Items.Any())
                cart.RemoveRestaurant(restaurantCart.RestaurantId);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

    }
}
