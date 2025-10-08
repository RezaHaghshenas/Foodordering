using Foodordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Events
{
    public class UserRoleChangedEvent : DomainEvent
    {
        public Guid UserId { get; }


        public UserRole Role { get; set; }

        public UserRoleChangedEvent(Guid userId, UserRole role)
        {
            UserId = userId;
            Role = role;
        }
    }
}
