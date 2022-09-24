
using Domain.Enums;

namespace Domain.Common
{
    internal abstract class BaseEmployee : BaseEntity
    {
        public string? Name { get; init; }

        public string? LastName { get; init; }

        public abstract Role Role { get; }
    }
}
