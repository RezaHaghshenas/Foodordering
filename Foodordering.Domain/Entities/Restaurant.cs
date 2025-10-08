using Foodordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrderingSystem.Domain.Entities
{
    public class Restaurant
    {
        public Guid Id { get; private set; }
        public string OwnerName { get; private set; } = string.Empty;
        public string OwnerFamily { get; private set; } = string.Empty;
        public string OwnerNationalCode { get; private set; } = string.Empty;
        public string OwnerPhone { get; private set; } = string.Empty;

        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string RestaurantPhone { get; private set; } = string.Empty;

        public bool IsActive { get; private set; } = true;
        public string? LogoUrl { get; private set; }
        public string? OpeningHours { get; private set; }
        public double Rating { get; private set; } = 0.0;

        public List<MenuItem>? MenuItems { get; private set; } = new();
        public List<OrderReview>? Reviews { get; private set; } = new();
        public Address? Address { get; private set; }        
        private Restaurant() { }

        public Restaurant(string ownerName, string ownerFamily, string ownerNationalCode,
                          string ownerPhone, string name, string description, string address,
                          string restaurantPhone, double latitude, double longitude)
        {
            Id = Guid.NewGuid();
            OwnerName = ownerName;
            OwnerFamily = ownerFamily;
            OwnerNationalCode = ownerNationalCode;
            OwnerPhone = ownerPhone;
            Name = name;
            Description = description;
            RestaurantPhone = restaurantPhone;

        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public void UpdateInfo(string name, string description,  string phone)
        {
            Name = name;
            Description = description;
            RestaurantPhone = phone;
        }

        public void UpdateLogo(string logoUrl) => LogoUrl = logoUrl;
        public void UpdateOpeningHours(string hours) => OpeningHours = hours;

        public void AddMenuItem(MenuItem item)
        {
            if (MenuItems.Any(x => x.Name == item.Name))
                throw new InvalidOperationException("Menu item already exists.");
            MenuItems.Add(item);
        }

        public void AddReview(OrderReview review)
        {
            Reviews.Add(review);
            Rating = Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;
        }
    }
}
