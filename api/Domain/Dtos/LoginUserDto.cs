using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class LoginUserDto
    {
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(100)]
        public string? Password { get; set; }
    }
}
