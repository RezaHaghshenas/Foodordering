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
    public class AddMenuItemCommandHandler : IRequestHandler<AddMenuItemCommand, Guid>
    {
        private readonly IAppDbContext _context;
        private readonly IEventPublisher _eventPublisher;

        public AddMenuItemCommandHandler(IAppDbContext context, IEventPublisher eventPublisher)
        {
            _context = context;
            _eventPublisher = eventPublisher;
        }

        public async Task<Guid> Handle(AddMenuItemCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _context.restaurant
                .FirstOrDefaultAsync(r => r.Id == request.RestaurantId, cancellationToken);

            if (restaurant == null)
                throw new InvalidOperationException("رستوران یافت نشد.");

            var item = new MenuItem(
                request.RestaurantId,
                request.Name,
                request.Description,
                request.Price,
                request.Category,
                request.PreparationTimeMinutes,
                request.ImageUrl
            );

            restaurant.AddMenuItem(item);

            foreach (var domainEvent in restaurant.DomainEvents)
                await _eventPublisher.PublishAsync(domainEvent);

            restaurant.DomainEvents.Clear();
            await _context.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
