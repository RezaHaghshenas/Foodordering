using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Events.User
{
    public class UserEmailChangedEvent : DomainEvent
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }


        public UserEmailChangedEvent(Guid userid , string email) 
        {
        Email = email;
            UserId = userid;
        }  

    }
}
