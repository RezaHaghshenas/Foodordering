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
    public class RemoveCartItemCommandHandler : IRequestHandler<RemoveCartItemCommand , bool>
    {
        private readonly IAppDbContext _context;
        public RemoveCartItemCommandHandler(IAppDbContext context)
        { 
        
        }



        public async Task<bool> Handle(RemoveCartItemCommand request,CancellationToken cancellationToken)
        {
            var cart = await _context.carts
                   .Include(c => c.Items)
                   .FirstOrDefaultAsync(c => c.Id == request.CartId, cancellationToken);

            if (cart == null)
                throw new InvalidOperationException("--- نشد");

            var SelectedItem =  cart.Items?.FirstOrDefault(u => u.Id == request.CartItemId);
            if (SelectedItem == null)
            {
                throw new InvalidOperationException("آیتم یافت نشد");
            }
            cart.RemoveCartItem(SelectedItem!);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
