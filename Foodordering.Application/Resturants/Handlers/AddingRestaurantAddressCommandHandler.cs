using Foodordering.Application.Common.DTOs.Address;
using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Resturants.Commands;
using Foodordering.Application.Users.Commands;
using Foodordering.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Resturants.Handlers
{
    public class AddingRestaurantAddressCommandHandler : IRequestHandler<AddingRestaurantAddressCommand, List<AddressDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ITokenService _tokenService;

        public AddingRestaurantAddressCommandHandler(IAppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<List<AddressDto>> Handle(AddingRestaurantAddressCommand request, CancellationToken cancellationToken)
        {
            var address = new Address(request.Street, request.City, request.Latitude, request.Longitude, request.IsDefault);
            _context.addresses.Add(address);
            await _context.SaveChangesAsync(cancellationToken);

            if (request.RestaurantId == null)
            {
                throw new InvalidOperationException("رستوران یافت نشد.");
            }

            return await _context.addresses.Where(wh => wh.RestaurantId == request.RestaurantId).Select(u => new AddressDto
            {
                City = u.City,
                Country = u.Country,
                Id = u.Id,
                IsDefault = u.IsDefault,
                Latitude = u.Latitude,
                Longitude = u.Longitude,
                PostalCode = u.PostalCode,
                Street = u.Street,
                Title = u.Title
            }
               ).ToListAsync();

        }

    }
}
