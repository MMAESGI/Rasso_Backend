using Common.Models;

namespace Identity.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? AvatarUrl { get; set; } = string.Empty;
        public ICollection<UserRoleEnum> role { get; set; } = new List<UserRoleEnum> { UserRoleEnum.Default };
    }
}
