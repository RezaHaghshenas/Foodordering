using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Entities
{
    public class LoginHistory
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string? DeviceId { get; private set; }
        public string? DeviceName { get; private set; }
        public string? IpAddress { get; private set; }
        public string? UserAgent { get; private set; }
        public DateTime LoggedInAt { get; private set; } = DateTime.UtcNow;
        public bool IsSuccessful { get; private set; }

        private LoginHistory() { } // EF

        public LoginHistory(Guid userId, string? deviceId, string? deviceName, string? ip, string? userAgent, bool isSuccessful)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            DeviceId = deviceId;
            DeviceName = deviceName;
            IpAddress = ip;
            UserAgent = userAgent;
            IsSuccessful = isSuccessful;
            LoggedInAt = DateTime.UtcNow;
        }
    }
}

