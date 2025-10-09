using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Events.User
{
    public class UserPhoneNumberChangedEvent : DomainEvent
    {

        public Guid UserId { get; }

        public string PhoneNumber { get; set; }

        public UserPhoneNumberChangedEvent(Guid userId ,  string phoneNumber)
        {
            UserId = userId;
            PhoneNumber = phoneNumber;
        }
    }
}
