using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.ValueObjects
{
    namespace FoodOrderingSystem.Domain.ValueObjects.Enums
    {
        public enum OrderStatus
        {
            Pending,
            Accepted,
            Preparing,
            ReadyForDelivery,
            OnTheWay,
            Delivered,
            Cancelled
        }

        public enum PaymentMethod
        {
            Cash,
            Online,
            Wallet
        }

        public enum DeliveryMethod
        {
            Pickup,
            Delivery
        }

        public enum PaymentStatus
        {
            Pending,
            Success,
            Failed,
            Refunded
        }

        public enum MenuCategory
        {
            Appetizer,
            MainCourse,
            Dessert,
            Drink
        }
    }

}
