using Foodordering.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Common.Interfaces
{

    public interface IEventPublisher
    {
        Task PublishAsync(DomainEvent domainEvent);
    }
}

