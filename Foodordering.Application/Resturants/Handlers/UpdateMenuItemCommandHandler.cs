using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Resturants.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Resturants.Handlers
{
    public class UpdateMenuItemCommandHandler : IRequestHandler<UpdateMenuItemCommand , bool>
    {
        private readonly IAppDbContext _context;

        public UpdateMenuItemCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _context.restaurant
                .Include(r => r.MenuItems)
                .FirstOrDefaultAsync(r => r.Id == request.RestaurantId, cancellationToken);

            if (restaurant == null)
                throw new InvalidOperationException("رستوران یافت نشد.");

            var item =  restaurant.MenuItems?.FirstOrDefault(x => x.Id == request.MenuItemId);
            if (item == null)
                throw new InvalidOperationException("آیتم منو یافت نشد.");

            if (!string.IsNullOrWhiteSpace(request.Name))
                item.UpdateName(request.Name);

            if (!string.IsNullOrWhiteSpace(request.Description))
                item.UpdateDescription(request.Description);

            if (!string.IsNullOrWhiteSpace(request.ImageUrl))
                item.UpdateImage(request.ImageUrl);

            if (request.Category.HasValue)
                item.UpdateCategory(request.Category.Value);

            if (request.PreparationTimeMinutes.HasValue)
                item.UpdatePreparationTime(request.PreparationTimeMinutes.Value);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
