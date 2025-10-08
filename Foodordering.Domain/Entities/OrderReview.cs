using FoodOrderingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Entities
{
    public class OrderReview
    {
        
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid RestaurantId { get; private set; }
        public Guid UserId { get; private set; }
        public double Rating { get; private set; }
        public string? Comment { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public Restaurant Restaurant { get; private set; } = null!;
        public User User { get; private set; } = null!;
        public Order order { get; private set; } = null!;

        private OrderReview() { }

        public OrderReview(Guid orderId, Guid restaurantId, Guid userId, double rating, string? comment)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            RestaurantId = restaurantId;
            UserId = userId;
            Rating = rating;
            Comment = comment;
        }
    }
}

