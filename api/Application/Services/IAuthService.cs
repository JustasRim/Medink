using Domain.Dtos;
using Domain.Entities;

namespace Application.Services
{
    public interface IAuthService
    {
        public User? ValidateUser(LoginUserDto user);

        Task<User?> SignUp(RegisterUserDto user);
    }
}
