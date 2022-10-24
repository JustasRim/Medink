using Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace api.Attributes
{
    public class AuthorizeRoles : AuthorizeAttribute
    {
        public AuthorizeRoles(params Role[] roles)
        {
            var role = roles.Select(q => Enum.GetName(typeof(Role), q));
            Roles = string.Join(",", role);
        }
    }
}
