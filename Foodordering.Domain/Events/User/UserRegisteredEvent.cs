using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Events.User
{
    using System.Text.Json.Serialization;

    public class UserRegisteredEvent : DomainEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public UserRegisteredEvent() { } // برای deserialization

        public UserRegisteredEvent(Guid userId, string email, string phoneNumber)
        {
            UserId = userId;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
