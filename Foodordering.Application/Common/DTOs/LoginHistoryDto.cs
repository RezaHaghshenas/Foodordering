using System;

namespace Foodordering.Application.Common.DTOs
{
    public class LoginHistoryDto
    {
        public DateTime LoggedInAt { get; set; }
        public string? DeviceName { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
