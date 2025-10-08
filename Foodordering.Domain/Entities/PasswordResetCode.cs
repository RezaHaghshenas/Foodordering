using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Entities
{
    public class PasswordResetCode
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; } = false;

        public User User { get; set; } = null!;

        private PasswordResetCode() { } // برای EF

        public PasswordResetCode(Guid userId, string code, DateTime expiresAt)
        {
            UserId = userId;
            Code = code;
            ExpiresAt = expiresAt;
            IsUsed = false;
        }

        public void MarkAsUsed()
        {
            IsUsed = true;
        }

        public bool IsValid()
        {
            return !IsUsed && ExpiresAt > DateTime.UtcNow;
        }
    }

}
