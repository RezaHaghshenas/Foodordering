using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Events
{
    public class UserPasswordResetEvent : DomainEvent
    {
        public Guid UserId { get; }

        public UserPasswordResetEvent(Guid userId) => UserId = userId;
    }
}
