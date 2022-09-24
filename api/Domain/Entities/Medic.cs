
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    internal class Medic : BaseEmployee
    {
        public override Role Role => Role.Medic;
    }
}
