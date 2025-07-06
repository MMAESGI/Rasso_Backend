using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class UserRole : IdentityUserRole<Guid>
    {
        [JsonIgnore]
        public required User User { get; set; }

        public required Role Role { get; set; }
    }

}
