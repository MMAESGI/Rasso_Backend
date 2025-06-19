using System.ComponentModel.DataAnnotations;

namespace Identity.Controllers.Requests
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

    }
}
