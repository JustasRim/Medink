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
        private readonly PasswordHasher<LoginUserDto> _hasher = new PasswordHasher<LoginUserDto>();

        private readonly IApplicationDbContext _context;

        public AuthService(IApplicationDbContext context)
        {
            _context = context;
        }

        public User? ValidateUser(LoginUserDto user)
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

        public async Task<User?> SignUp(RegisterUserDto user)
        {
            var currentUser = GetUser(user);
            if (currentUser != null || user.Role == Role.Admin)
            {
                return null;
            }

            var saltedHash = _hasher.HashPassword(user, user.Password);
            var newUser = new User
            {
                Email = user.Email,
                SaltedHash = saltedHash,
                Role = user.Role
            };

            switch(user.Role)
            {
                case Role.Medic:
                    var medic = new Medic
                    {
                        Email = user.Email,
                        LastName = user.LastName,
                        Name = user.Name
                    };

                    _context.Medics.Add(medic);
                    break;
                case Role.Patient:
                    var patient = new Patient
                    {
                        Email = user.Email,
                        LastName = user.LastName,
                        Name = user.Name
                    };

                    _context.Patients.Add(patient);
                    break;
            }

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        private User? GetUser(LoginUserDto user)
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
