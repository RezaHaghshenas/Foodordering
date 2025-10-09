using Foodordering.Domain.Events.Restaurant;
using Foodordering.Domain.Events;
using Foodordering.Domain.ValueObjects.FoodOrderingSystem.Domain.ValueObjects.Enums;
using System;
using FoodOrderingSystem.Domain.Entities;
using System.Xml.Linq;

namespace FoodOrderingSystem.Domain.Entities
{
    public class MenuItem
    {
        public Guid Id { get; private set; }
        public Guid RestaurantId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public bool IsActive { get;  set; } = true;
        public string? ImageUrl { get; private set; }
        public MenuCategory Category { get; private set; } = MenuCategory.MainCourse;
        public int PreparationTimeMinutes { get; private set; }

        public List<DomainEvent> DomainEvents { get; private set; } = new List<DomainEvent>();

        public Restaurant Restaurant { get; private set; } = null!;

        private MenuItem() { }

        public MenuItem(Guid restaurantId, string name, string description, decimal price,
                        MenuCategory category, int prepTime, string? imageUrl = null)
        {
            Id = Guid.NewGuid();
            RestaurantId = restaurantId;
            Name = name;
            Description = description;
            Price = price;
            Category = category;
            PreparationTimeMinutes = prepTime;
            ImageUrl = imageUrl;
        }


        public void Activate()
        {
            IsActive = true; 
        }

        public void Deactivate()
        {
            IsActive = false;
        }



        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0) throw new ArgumentException("Invalid price");
            Price = newPrice;
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("نام نمی‌تواند خالی باشد.");

            Name = newName;
        }

        public void UpdateDescription(string newDescription)
        {
            Description = newDescription;
        }

        public void UpdateImage(string imageUrl)
        {
            ImageUrl = imageUrl;
        }

        public void UpdateCategory(MenuCategory category)
        {
            Category = category;
        }

        public void UpdatePreparationTime(int minutes)
        {
            if (minutes == 0)
                throw new ArgumentException("minutes نمی‌تواند خالی باشد.");
            PreparationTimeMinutes = minutes;
        }
    }
}




