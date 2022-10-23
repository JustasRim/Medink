using Domain.Dtos;
using Domain.Entities;

namespace Application.Helpers
{
    public interface IAuthHelper
    {
        TokenDto GetAuthToken(User user);
    }
}
