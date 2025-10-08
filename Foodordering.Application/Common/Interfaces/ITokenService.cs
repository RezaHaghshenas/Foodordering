using Foodordering.Application.Common.DTOs;
using Foodordering.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<AuthResultDto> GenerateTokens(User user, string? deviceId, string? deviceName, string? ipAddress, string? userAgent);
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        public  Task<User?> RevokeRefreshTokenAsync(string refreshToken, string? deviceId = null, string? ipAddress = null);

        public Task<bool> RevokeAllRefreshTokenAsync(string refreshToken, string? deviceId = null, string? ipAddress = null);
        Task SaveRefreshTokenAsync(User user, string refreshToken, string? deviceId, string? deviceName, string? ipAddress, string? userAgent);

        public  Task<bool?> RevokeRefreshTokenByIdAsync(Guid UserId, string? deviceId = null, string? ipAddress = null);

    }
}
