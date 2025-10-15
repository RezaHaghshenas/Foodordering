using Foodordering.Application.Carts.Command;
using Foodordering.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Common.Carts.Handlers
{
    public class RemoveRestaurantCartCommandHandler : IRequestHandler<RemoveRestaurantCartCommand , bool>
    {
        private readonly IAppDbContext _context;
        public RemoveRestaurantCartCommandHandler(IAppDbContext context)
        {
            _context = context; 
        }



        public async Task<bool> Handle(RemoveRestaurantCartCommand request,CancellationToken cancellationToken)
        {
            var cart = await _context.carts
                   .Include(c => c.RestaurantCarts)
                   .FirstOrDefaultAsync(c => c.Id == request.CartId, cancellationToken);

            if (cart == null)
                throw new InvalidOperationException("سبد خرید مورد نظر یافت نشد");


            var restaurantCart = cart.RestaurantCarts.FirstOrDefault(rc => rc.RestaurantId == request.RestaurantId);

            if (restaurantCart == null)
            {
                throw new InvalidOperationException("رستوران یافت نشد");
            }
            cart.RemoveRestaurant(restaurantCart.RestaurantId);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
