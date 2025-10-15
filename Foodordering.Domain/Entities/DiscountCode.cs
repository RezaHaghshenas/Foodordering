using Foodordering.Domain.ValueObjects.FoodOrderingSystem.Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Entities
{
    public class DiscountCode
    {
        public Guid Id { get; set; }
        public string Code { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public decimal? Amount { get; private set; }
        public bool IsPercentage { get; private set; }

        public List<MenuCategory> ApplicableCategories { get; private set; } = new();
        public List<Guid> ApplicableRestaurantIds { get; private set; } = new();

        // 🔥 لیست مشتری‌هایی که مجاز به استفاده هستن
        public List<Guid> AllowedUserIds { get; private set; } = new();

        public bool IsValidForUser(Guid userId)
        {
            return AllowedUserIds.Count == 0 || AllowedUserIds.Contains(userId);
        }
    }

}
