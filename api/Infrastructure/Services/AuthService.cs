using api.Application.Common.Interfaces;
using Application.Services;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly PasswordHasher<AuthenticateUserDto> _hasher = new PasswordHasher<AuthenticateUserDto>();

        private readonly IApplicationDbContext _context;

        public AuthService(IApplicationDbContext context)
        {
            _context = context;
        }

        public User? ValidateUser(AuthenticateUserDto user)
        {
            var currentUser = GetUser(user);
            if (currentUser == null)
            {
                return null;
            }

            var authResult = _hasher.VerifyHashedPassword(user, currentUser.SaltedHash, user.Password);
            if (authResult == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return currentUser;
        }

        public async Task<User?> SignUp(AuthenticateUserDto user)
        {
            var currentUser = GetUser(user);
            if (currentUser != null)
            {
                return null;
            }

            var saltedHash = _hasher.HashPassword(user, user.Password);
            var newUser = new User
            {
                Email = user.Email,
                SaltedHash = saltedHash,
                Role = Role.Patient
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        private User? GetUser(AuthenticateUserDto user)
        {
            if (user == null)
            {
                return null;
            }

            var currentUser = _context.Users.FirstOrDefault(q => q.Email == user.Email);
            return currentUser;
        }
    }
}
