using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(20)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(20)]
        public string LastName { get; set; } = null!;

        [Required, MaxLength(150), EmailAddress]
        public string Email { get; set; } = null!;

        [Required, MaxLength(20)]
        public string Username { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public bool IsActive { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? AnonymizedAt { get; set; }
    }
}
