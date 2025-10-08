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
    public class AddingAddressCommandHandler : IRequestHandler<AddingAddressCommand , List<AddressDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ITokenService _tokenService;

        public AddingAddressCommandHandler(IAppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<List<AddressDto>> Handle(AddingAddressCommand request , CancellationToken cancellationToken)
        {
            var address = new Address(request.Street, request.City, request.Latitude, request.Longitude, request.IsDefault); 
            _context.addresses.Add(address);
           await _context.SaveChangesAsync(cancellationToken);

            if (request.UserId != null)
            {
              return await _context.addresses.Where(wh => wh.UserId == request.UserId).Select(u=> new AddressDto
                { 
                City = u.City,  
                Country = u.Country,
                Id = u.Id,
                IsDefault = u.IsDefault,    
                Latitude = u.Latitude , 
                Longitude = u.Longitude , 
                 PostalCode = u.PostalCode ,
                 Street = u.Street ,
                 Title = u.Title        
                }
                ).ToListAsync(); 
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
