using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public Role Role { get; set; }

        public string? SaltedHash { get; set; }

        public string? Email { get; set; }
    }
}
