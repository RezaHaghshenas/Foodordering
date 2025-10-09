using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Events.User
{
    public class UserSecurityStampChangedEvent : DomainEvent
    {
        public Guid UserId { get; }

        public UserSecurityStampChangedEvent(Guid userId) => UserId = userId;
    }
}
