using System;
using System.Collections.Generic;

namespace Foodordering.Application.Common.DTOs
{
    public class UserDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime RegisteredAt { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Notes { get; set; }

        // ✅ برای نمایش جزئیات بیشتر
        public List<RefreshTokenDto> ActiveTokens { get; set; } = new();
        public List<PasswordResetCodeDto> PasswordResetCodes { get; set; } = new();
    }

    public class RefreshTokenDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        public bool IsActive => RevokedAt == null;
    }

    public class PasswordResetCodeDto
    {
        public string Code { get; set; } = string.Empty;
        public bool IsUsed { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
