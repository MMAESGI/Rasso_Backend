using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{

    /// <inherits from="IdentityRole{Guid}">The base class for roles in the identity system.</inherits>
    public class Role : IdentityRole<Guid>
    {
    }

}
