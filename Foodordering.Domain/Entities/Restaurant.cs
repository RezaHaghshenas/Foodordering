using Foodordering.Domain.Entities;
using Foodordering.Domain.Events;
using Foodordering.Domain.Events.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using Foodordering.Domain.Events.User;
using System.Numerics;
using System.Xml.Linq;
using Foodordering.Domain.ValueObjects.FoodOrderingSystem.Domain.ValueObjects.Enums;
using FoodOrderingSystem.Domain.Entities;

namespace FoodOrdering.Domain.Entities
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

        public List<DomainEvent> DomainEvents { get; private set; } = new List<DomainEvent>();
        public List<MenuItem>? MenuItems { get; private set; } = new();
        public List<OrderReview>? Reviews { get; private set; } = new();
        public Address? Address { get; private set; }        
        private Restaurant() { }

        public Restaurant(string ownerName, string ownerFamily, string ownerNationalCode,
                          string ownerPhone, string name, string description,
                          string restaurantPhone)
        {
            Id = Guid.NewGuid();
            OwnerName = ownerName;
            OwnerFamily = ownerFamily;
            OwnerNationalCode = ownerNationalCode;
            OwnerPhone = ownerPhone;
            Name = name;
            Description = description;
            RestaurantPhone = restaurantPhone;

            DomainEvents.Add(new RestaurantCreatedEvent(Id,OwnerName , OwnerFamily , OwnerNationalCode));
        }

        public void Activate()
        {
            IsActive = true;
            DomainEvents.Add(new RestaurantActivatedEvent(Id)); 
        }
        public void Deactivate()
        {
            IsActive = false;
            DomainEvents.Add(new RestaurantDeactivatedEvent(Id));
        }


        public void MenuItemActivate(Guid itemId)
        {

            var item = MenuItems?.FirstOrDefault(x => x.Id == itemId);
            if (item == null)
                throw new InvalidOperationException("Menu item not found.");

            item.Activate();    
            DomainEvents.Add(new MenuItemsActivatedEvent(Id, Name));
        }


        public void MenuItemDeactivate(Guid itemId)
        {
            var item = MenuItems?.FirstOrDefault(x => x.Id == itemId);
            if (item == null)
                throw new InvalidOperationException("Menu item not found.");
            item.Deactivate();
            DomainEvents.Add(new MenuItemsDeactivatedEvent(Id , Name));
        }

        public void UpdateInfo(string name, string description,  string phone)
        {
            Name = name;
            Description = description;
            RestaurantPhone = phone;
            DomainEvents.Add(new RestaurantInfoUpdatedEvent(Id , name , description , phone));
        }

        public void UpdateLogo(string logoUrl) => LogoUrl = logoUrl;
        public void UpdateOpeningHours(string hours) => OpeningHours = hours;

        public void AddMenuItem(MenuItem item)
        {
            if (MenuItems.Any(x => x.Name == item.Name))
                throw new InvalidOperationException("Menu item already exists.");
            MenuItems.Add(item);
            DomainEvents.Add(new MenuItemAddedEvent(Id , item.Id , item.Name));
        }

        public void AddReview(OrderReview review)
        {
            Reviews.Add(review);
            Rating = Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;
        }

        public bool HasMenuItem(string name)
        {
            return MenuItems?.Any(x => x.Name == name) ?? false;
        }


        public void RemoveMenuItem(Guid itemId)
        {
            var item = MenuItems?.FirstOrDefault(x => x.Id == itemId);
            if (item == null)
                throw new InvalidOperationException("Menu item not found.");

            MenuItems!.Remove(item);
            DomainEvents.Add(new MenuItemRemovedEvent(Id , Name));
        }


        public void UpdateAddress(Address newAddress)
        {
            Address = newAddress ?? throw new ArgumentNullException(nameof(newAddress));
        }


        public void ClearReviews()
        {
            Reviews?.Clear();
            Rating = 0;
        }



        public double GetAverageRating()
        {
            return Reviews?.Any() == true ? Reviews.Average(r => r.Rating) : 0;
        }


        public bool IsOpenAt(TimeSpan time)
        {
            // فرض بر اینه که OpeningHours به شکل "08:00-22:00" ذخیره شده
            if (string.IsNullOrWhiteSpace(OpeningHours)) return false;

            var parts = OpeningHours.Split('-');
            if (parts.Length != 2) return false;

            if (TimeSpan.TryParse(parts[0], out var start) && TimeSpan.TryParse(parts[1], out var end))
            {
                return time >= start && time <= end;
            }

            return false;
        }


    }
}
