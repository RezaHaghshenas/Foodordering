using Foodordering.Application.Carts.Command;
using Foodordering.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Foodordering.Application.Common.Carts.Handlers
{
    public class ApplyDiscountToRestaurantCartCommandHandler : IRequestHandler<ApplyDiscountToRestaurantCartCommand, bool>
    {
        private readonly IAppDbContext _context;

        public ApplyDiscountToRestaurantCartCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ApplyDiscountToRestaurantCartCommand request, CancellationToken cancellationToken)
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

            restaurantCart.ApplyDiscount(request.DiscountAmount);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
