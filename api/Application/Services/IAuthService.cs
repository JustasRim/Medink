using Domain.Dtos;
using Domain.Entities;

namespace Application.Services
{
    public interface IAuthService
    {
        public User? ValidateUser(AuthenticateUserDto user);

        Task<User?> SignUp(AuthenticateUserDto user);
    }
}
