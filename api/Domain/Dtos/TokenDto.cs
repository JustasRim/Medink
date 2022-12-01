using Domain.Enums;

namespace Domain.Dtos
{
    public class TokenDto
    {
        public string? Token { get; set; }
        public DateTime Expires { get; set; }
        public Role Role { get; set; }
    }
}
