using Foodordering.Application.Common.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Foodordering.Application.Users.Queries
{
    public class SearchUsersQuery : IRequest<List<UserDto>>
    {
        public string? Keyword { get; set; } // می‌تونه نام، ایمیل یا شماره موبایل باشه
        public bool? IsActive { get; set; }  // فیلتر اختیاری برای کاربران فعال یا غیرفعال
        public int? Role { get; set; }       // فیلتر اختیاری بر اساس نقش (0:Customer, 1:Delivery, 2:Admin)
    }
}
