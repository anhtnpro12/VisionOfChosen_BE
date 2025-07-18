using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using VisionOfChosen_BE.Configurations;
using VisionOfChosen_BE.Infra.Models;
using VisionOfChosen_BE.Infra.Context;
using VisionOfChosen_BE.DTOs.Auth;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VisionOfChosen_BE.Infra.Consts;
using VisionOfChosen_BE.Authentication;

namespace VisionOfChosen_BE.Services
{
    public interface IAuthService
    {
        string? Authenticate(string email, string password);
        Task<bool> RegisterAsync(RegisterRequestDto request);
    }

    public class AuthService : IAuthService
    {
        private readonly VisionOfChosen_Context _context;
        private readonly JwtSettings _jwt;
        private readonly PasswordHasher<User> _hasher;

        public AuthService(VisionOfChosen_Context context, IOptions<JwtSettings> jwtOptions)
        {
            _context = context;
            _jwt = jwtOptions.Value;
            _hasher = new PasswordHasher<User>();
        }

        public string? Authenticate(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            if (user == null) return null;

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed) return null;

            var claims = new[]
            {
                new Claim(CustomClaimTypes.Email, user.Email),
                new Claim(CustomClaimTypes.UserId, user.id),
                new Claim(CustomClaimTypes.Role, user.Role),
                new Claim(CustomClaimTypes.Name, user.Name ?? ""),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ExpiresInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> RegisterAsync(RegisterRequestDto request)
        {
            // Kiểm tra email đã tồn tại chưa
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return false;

            // Tạo user mới
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Role = RoleConst.User
            };
            user.PasswordHash = _hasher.HashPassword(user, request.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
