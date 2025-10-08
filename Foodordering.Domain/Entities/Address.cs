using FoodOrderingSystem.Domain.Entities;
using System;

namespace Foodordering.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
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

        public User? User { get; set; }
        public Restaurant? Restaurant { get; set; }

        private Address() { }

        public Address(string street, string city, double lat, double lng, bool isDefault = false)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street is required");
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City is required");

            Street = street;
            City = city;
            Latitude = lat;
            Longitude = lng;
            IsDefault = isDefault;
        }
    }
}
