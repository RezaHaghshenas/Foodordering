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
    public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, bool>
    {
        private readonly IAppDbContext _context;

        public ClearCartCommandHandler(IAppDbContext context)
        {
            _context = context;
        }


        public async Task<bool> Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _context.carts
                .Include(c => c.RestaurantCarts)
                .FirstOrDefaultAsync(c => c.UserId == request.UserId && c.Id == request.CartId, cancellationToken);

            if (cart == null)
                return false;

            if (!cart.RestaurantCarts.Any())
                return true; // Already empty

            cart.Clear();

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }


    }
}
