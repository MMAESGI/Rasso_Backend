using System.ComponentModel.DataAnnotations;

namespace Identity.Controllers.Requests
{
    public class SignUpRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "FirstName name cannot be longer than 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "LastName name cannot be longer than 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(20, ErrorMessage = "Username cannot be longer than 20 characters.")]
        public string Username { get; set; } = string.Empty;




        //[Required]
        //[StringLength(15, ErrorMessage = "Phone number cannot be longer than 15 characters.")]
        //public string PhoneNumber { get; set; } = string.Empty;

        //[Required]
        //[StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters.")]
        //public string Address { get; set; } = string.Empty;

        //[Required]
        //[StringLength(50, ErrorMessage = "City cannot be longer than 50 characters.")]
        //public string City { get; set; } = string.Empty;

        //[Required]
        //[StringLength(50, ErrorMessage = "State cannot be longer than 50 characters.")]
        //public string State { get; set; } = string.Empty;

        //[Required]
        //[StringLength(10, ErrorMessage = "Postal code cannot be longer than 10 characters.")]
        //public string PostalCode { get; set; } = string.Empty;

        //[Required]
        //[StringLength(50, ErrorMessage = "Country cannot be longer than 50 characters.")]

        //public string Country { get; set; } = string.Empty;

        }
}
