using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Events.User
{
    public class UserPasswordChangedEvent : DomainEvent
    {
        public Guid UserId { get; }

        public UserPasswordChangedEvent(Guid userId) => UserId = userId;
    }
}
