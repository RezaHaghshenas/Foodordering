using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Events.User
{
    public class UserDeactivatedEvent : DomainEvent
    {
        public Guid UserId { get; }

        public UserDeactivatedEvent() { }

        public UserDeactivatedEvent(Guid userId) => UserId = userId;
    }
}
