using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public required User User { get; set; }

        public required Role Role { get; set; }
    }

}
