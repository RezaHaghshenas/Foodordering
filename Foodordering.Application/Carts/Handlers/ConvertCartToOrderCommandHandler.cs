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
    public class ConvertCartToOrdersCommandHandler : IRequestHandler<ConvertCartToOrdersCommand, List<Guid>>
    {
        private readonly IAppDbContext _context;

        public ConvertCartToOrdersCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Guid>> Handle(ConvertCartToOrdersCommand request, CancellationToken cancellationToken)
        {
            var cart = await _context.carts
                .Include(c => c.RestaurantCarts)
                    .ThenInclude(rc => rc.Items)
                .FirstOrDefaultAsync(c => c.Id == request.CartId, cancellationToken);

            if (cart == null)
                throw new InvalidOperationException("سبد خرید یافت نشد");

            var orders = cart.ToOrders(request.DeliveryAddress, request.CustomerPhone);

            foreach (var order in orders)
            {
                _context.orders.Add(order);
            }

            // اختیاری: پاک‌سازی سبد بعد از تبدیل
            cart.Clear();

            await _context.SaveChangesAsync(cancellationToken);

            return orders.Select(o => o.Id).ToList();
        }
    }

}
