using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Resturants.Commands;
using FoodOrderingSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Resturants.Handlers
{
    public class DeactivateMenuItemCommandHandler : IRequestHandler<DeactivateMenuItemCommand , bool>
    {
        private readonly IAppDbContext _context;
        private readonly IEventPublisher _eventPublisher;

        public DeactivateMenuItemCommandHandler(IAppDbContext context, IEventPublisher eventPublisher)
        {
            _context = context;
            _eventPublisher = eventPublisher;
        }


        public async Task<bool> Handle(DeactivateMenuItemCommand request, CancellationToken cancellationToken)
        {
            
            var restaurant = await _context.restaurant
                 .Include(r => r.MenuItems)
                 .FirstOrDefaultAsync(r => r.Id == request.ResturantId, cancellationToken);

            if (restaurant == null)
                throw new InvalidOperationException("رستوران یافت نشد.");

            var item = restaurant.MenuItems?.FirstOrDefault(x => x.Id == request.MenuItemId);
            if (item == null)
                throw new InvalidOperationException("آیتم منو یافت نشد.");

            restaurant.MenuItemDeactivate(item.Id);

            foreach (var domainEvent in restaurant.DomainEvents)
            {
                await _eventPublisher.PublishAsync(domainEvent);
            }

            restaurant.DomainEvents.Clear();

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
}
