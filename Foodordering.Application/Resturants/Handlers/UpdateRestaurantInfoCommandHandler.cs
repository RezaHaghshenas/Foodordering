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

    public class UpdateRestaurantInfoCommandHandler : IRequestHandler<UpdateRestaurantInfoCommand , bool>
    {
        private readonly IAppDbContext _context;

        public UpdateRestaurantInfoCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateRestaurantInfoCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _context.restaurant
                .FirstOrDefaultAsync(r => r.Id == request.RestaurantId, cancellationToken);

            if (restaurant == null)
                throw new InvalidOperationException("رستوران یافت نشد.");


            restaurant.UpdateInfo(restaurant.Name, request.Description, request.RestaurantPhone); 

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}




