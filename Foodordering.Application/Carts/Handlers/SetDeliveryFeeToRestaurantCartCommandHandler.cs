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
    public class SetDeliveryFeeToRestaurantCartCommandHandler : IRequestHandler<SetDeliveryFeeToRestaurantCartCommand, bool>
    {
        private readonly IAppDbContext _context;

        public SetDeliveryFeeToRestaurantCartCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(SetDeliveryFeeToRestaurantCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _context.carts
                .Include(c => c.RestaurantCarts)
                .FirstOrDefaultAsync(c => c.Id == request.CartId, cancellationToken);

            if (cart == null)
                throw new InvalidOperationException("سبد خرید یافت نشد");

            var restaurantCart = cart.RestaurantCarts
                .FirstOrDefault(rc => rc.RestaurantId == request.RestaurantId);

            if (restaurantCart == null)
                throw new InvalidOperationException("سبد مربوط به این رستوران یافت نشد");

            restaurantCart.SetDeliveryFee(request.DeliveryFee);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
