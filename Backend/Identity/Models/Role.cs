using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = default!;

        // Référence inverse pour avoir accès aux utilisateurs associés à ce rôle
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

}
