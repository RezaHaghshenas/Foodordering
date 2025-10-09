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
    public class UpdateOpeningHoursCommandHandler : IRequestHandler<UpdateOpeningHoursCommand>
    {
        private readonly IAppDbContext _context;
        private readonly IEventPublisher _eventPublisher;

        public UpdateOpeningHoursCommandHandler(IAppDbContext context, IEventPublisher eventPublisher)
        {
            _context = context;
            _eventPublisher = eventPublisher;
        }

        public async Task<Unit> Handle(UpdateOpeningHoursCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _context.restaurant
                .FirstOrDefaultAsync(r => r.Id == request.RestaurantId, cancellationToken);

            if (restaurant == null)
                throw new InvalidOperationException("رستوران یافت نشد.");

            restaurant.UpdateOpeningHours(request.OpeningHours);

            foreach (var domainEvent in restaurant.DomainEvents)
                await _eventPublisher.PublishAsync(domainEvent);

            restaurant.DomainEvents.Clear();
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
