using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    [Index(nameof(Email), IsUnique = true)]
    /// <inherits from="IdentityUser{Guid}">The base class for users in the identity system.</inherits>
    public class User : IdentityUser<Guid>
    {

        [Required, MaxLength(20), ProtectedPersonalData]
        public string? FirstName { get; set; }

        [Required, MaxLength(20), ProtectedPersonalData]
        public string? LastName { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public bool IsActive { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? AnonymizedAt { get; set; }
    }
}
