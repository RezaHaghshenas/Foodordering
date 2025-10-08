using Foodordering.Application.Common.DTOs.Address;
using Foodordering.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Users.Commands
{
    public class AddingAddressCommand : IRequest<List<AddressDto>>
    {
        public string? Title { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsDefault { get; set; }

        // 🔹 Owner info
        public Guid? UserId { get; set; }
        public Guid? RestaurantId { get; set; }
    }
}
