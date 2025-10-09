using Foodordering.Application.Common.DTOs.Address;
using Foodordering.Application.Common.Interfaces;
using Foodordering.Application.Users.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Handlers
{
    public class MarkUserAddressDefaultCommandHandler : IRequestHandler<MarkUserAddressDefaultCommand, List<AddressDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ITokenService _tokenService;

        public MarkUserAddressDefaultCommandHandler(IAppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<List<AddressDto>> Handle(MarkUserAddressDefaultCommand request, CancellationToken cancellationToken)
        {
            var address = await _context.addresses.FirstOrDefaultAsync(f => f.Id == request.Address_Id);
            if (address == null)
            {
                throw new Exception("آدرسی با این مشخصات پیدا نشد");
            }
            var other_addresses = new List<AddressDto>();
            if (address.UserId == null)
            {
                throw new InvalidOperationException("کاربر یافت نشد.");
            }
            other_addresses = await _context.addresses.Where(wh => wh.UserId == address.UserId).Select(u => new AddressDto
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

            foreach (var item in other_addresses)
            {
                item.IsDefault = false;
            }
            address.IsDefault = true;
            await _context.SaveChangesAsync(cancellationToken);
            return other_addresses;
        }
    }
}
