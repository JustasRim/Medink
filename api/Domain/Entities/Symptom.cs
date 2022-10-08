using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Symptom : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        public int? PatientId { get; set; }

        public Patient? Patient { get; set; }
    }
}
