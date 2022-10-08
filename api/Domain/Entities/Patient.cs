using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Patient : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [StringLength(100)]
        public string? LastName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Phone]
        public string? Number { get; set; }

        public int? MedicId { get; set; }

        public Medic? Medic { get; set; }

        public IList<Symptom>? Symptoms { get; set; }
    }
}
