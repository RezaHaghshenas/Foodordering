using Foodordering.Application.Common.DTOs;
using Foodordering.Application.Common.Interfaces;
using Foodordering.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Infrastructure.Identity
{



    public class TokenService : ITokenService
    {
        private readonly IAppDbContext _context;
        private readonly IConfiguration _configuration;



        

        public TokenService(IAppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // JWT access token generation
        public string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!);

            var claims = new List<Claim>
            { 
                 new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                 new Claim("phone_number", user.PhoneNumber),
                 new Claim("role", user.Role.ToString()),
                 new Claim("security_stamp", user.SecurityStamp)
            };


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15), // Access token 15 دقیقه
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        public async Task<AuthResultDto> GenerateTokens(User user, string? deviceId, string? deviceName, string? ipAddress, string? userAgent)
        {
            var AccessToken = GenerateAccessToken(user);      
            var RefreshToken = GenerateRefreshToken();

            await SaveRefreshTokenAsync(user, RefreshToken, deviceId, deviceName, ipAddress, userAgent);

            return new AuthResultDto()
            {
                RefreshToken = RefreshToken,
                AccessToken = AccessToken,
                AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(15),
                RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(30), 
                UserId = user.Id,
            };
        }

        public async Task<bool> RevokeAllRefreshTokenAsync(string refreshToken, string? deviceId = null, string? ipAddress = null)
        {
            var tokens = _context.RefreshTokens
                .Where(rt => deviceId == null || rt.DeviceId == deviceId)
                .ToList(); // الآن در memory

            var tokenEntity = tokens.FirstOrDefault(rt => BCrypt.Net.BCrypt.Verify(refreshToken, rt.TokenHash));

            if (tokenEntity == null)
                return false;

            tokenEntity.Revoked = DateTime.UtcNow;
            tokenEntity.RevokedByIp = ipAddress;


            var All_RefreshTokens = await _context.RefreshTokens.Where(wh => wh.UserId == tokenEntity.UserId).ToListAsync();

            foreach (var item in All_RefreshTokens)
            {
                item.Revoked = DateTime.UtcNow;
                item.RevokedByIp = ipAddress;
            }

            await _context.SaveChangesAsync(default);

            return true; 
        }

        public async Task<User?> RevokeRefreshTokenAsync(string refreshToken, string? deviceId = null, string? ipAddress = null)
        {
            var tokens = _context.RefreshTokens
        .Where(rt => deviceId == null || rt.DeviceId == deviceId)
        .ToList(); // الآن در memory

            var tokenEntity = tokens.FirstOrDefault(rt => BCrypt.Net.BCrypt.Verify(refreshToken, rt.TokenHash));

            if (tokenEntity == null)
                return null;

            tokenEntity.Revoked = DateTime.UtcNow;
            tokenEntity.RevokedByIp = ipAddress;

            await _context.SaveChangesAsync(default);

            return tokenEntity.User;
        }



        public async Task<bool?> RevokeRefreshTokenByIdAsync(Guid UserId, string? deviceId = null, string? ipAddress = null)
        {
            var All_RefreshTokens = await _context.RefreshTokens.Where(wh => wh.UserId == UserId).ToListAsync();

            foreach (var item in All_RefreshTokens)
            {
                item.Revoked = DateTime.UtcNow;
                item.RevokedByIp = ipAddress;
            }

            await _context.SaveChangesAsync(default);

            return true;
        }










        public async Task SaveRefreshTokenAsync(User user, string refreshToken, string? deviceId, string? deviceName, string? ipAddress, string? userAgent)
        {
            var tokenEntity = new RefreshToken
            {
                UserId = user.Id,
                TokenHash = BCrypt.Net.BCrypt.HashPassword(refreshToken),
                DeviceId = deviceId,
                DeviceName = deviceName,
                CreatedByIp = ipAddress,
                UserAgent = userAgent,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(30) // Refresh token 30 روز
            };
            _context.RefreshTokens.Add(tokenEntity);
            await _context.SaveChangesAsync(default);
        }
    }
}
