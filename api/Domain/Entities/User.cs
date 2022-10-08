using Domain.Enums;
using System.Security.Claims;

namespace Domain.Entities
{
    internal class User
    {
        public List<Claim>? Claim { get; set; }
        public Role Role { get; set; }

        public string? SaltedHash { get; set; }

        public string? Email { get; set; }
    }
}
