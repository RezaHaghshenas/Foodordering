using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Resturants.Commands;
using FoodOrderingSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Resturants.Handlers
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, Guid>
    {
        private readonly IAppDbContext _context;
        private readonly IEventPublisher _eventPublisher;

        public CreateRestaurantCommandHandler(IAppDbContext context, IEventPublisher eventPublisher)
        {
            _context = context;
            _eventPublisher = eventPublisher;
        }

        public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = new Restaurant(
                request.OwnerName,
                request.OwnerFamily,
                request.OwnerNationalCode,
                request.OwnerPhone,
                request.Name,
                request.Description,
                request.RestaurantPhone
            );

            _context.restaurant.Add(restaurant);
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in restaurant.DomainEvents)
            {
                await _eventPublisher.PublishAsync(domainEvent); // Kafka
            }

            restaurant.DomainEvents.Clear();

            return restaurant.Id;
        }
    }

}
