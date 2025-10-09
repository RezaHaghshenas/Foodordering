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


    public class ActivateMenuItemCommandHandler : IRequestHandler<ActivateMenuItemCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly IEventPublisher _eventPublisher;

        public ActivateMenuItemCommandHandler(IAppDbContext context, IEventPublisher eventPublisher)
        {
            _context = context;
            _eventPublisher = eventPublisher;
        }


        public async Task<bool> Handle(ActivateMenuItemCommand request, CancellationToken cancellationToken)
        {

            var restaurant = await _context.restaurant
                 .Include(r => r.MenuItems)
                 .FirstOrDefaultAsync(r => r.Id == request.ResturantId, cancellationToken);

            if (restaurant == null)
                throw new InvalidOperationException("رستوران یافت نشد.");

            var item = restaurant.MenuItems?.FirstOrDefault(x => x.Id == request.MenuItemId);
            if (item == null)
                throw new InvalidOperationException("آیتم منو یافت نشد.");

            restaurant.MenuItemActivate(item.Id);

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
