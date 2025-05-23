using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class UserRole
    {
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string Code { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Label { get; set; } = null!;
    }
}
