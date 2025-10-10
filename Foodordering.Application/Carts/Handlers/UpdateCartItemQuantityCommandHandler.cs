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
    public class UpdateCartItemQuantityCommandHandler : IRequestHandler<UpdateCartItemQuantityCommand, bool>
    {
        private readonly IAppDbContext _context;

        public UpdateCartItemQuantityCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
        {
            var cart = await _context.carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == request.CartId, cancellationToken);

            if (cart == null)
                throw new InvalidOperationException("سبد یافت نشد");

            var item = cart.Items.FirstOrDefault(i => i.Id == request.CartItemId);
            if (item == null)
                throw new InvalidOperationException("چنین آیتمی یافت نشد");

            if (request.NewQuantity <= 0)
            {
                cart.RemoveCartItem(item);

                if (cart.Items.Count < 1)
                {
                    cart.MarkForDeletion(); // متد دامنه‌ای
                    _context.carts.Remove(cart); // حذف از دیتابیس
                }
            }
            else
            {
                item.UpdateQuantity(request.NewQuantity);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
