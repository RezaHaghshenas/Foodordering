using Foodordering.Application.Common.DTOs.Address;
using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Users.Commands;
using Foodordering.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Handlers
{
    public class AddingUserAddressCommandHandler : IRequestHandler<AddingUserAddressCommand , List<AddressDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ITokenService _tokenService;

        public AddingUserAddressCommandHandler(IAppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<List<AddressDto>> Handle(AddingUserAddressCommand request , CancellationToken cancellationToken)
        {
            var address = new Address(request.Street, request.City, request.Latitude, request.Longitude, request.IsDefault); 
            _context.addresses.Add(address);
           await _context.SaveChangesAsync(cancellationToken);

            if (request.UserId == null)
            {
                throw new InvalidOperationException("کاربر یافت نشد.");
            }

            return await _context.addresses.Where(wh => wh.UserId == request.UserId).Select(u => new AddressDto
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
