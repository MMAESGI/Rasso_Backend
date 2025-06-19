namespace Identity.DTOs.Responses
{
    public class DetailedUserResponse : UserResponse
    {
        public Guid Id { get; set; }
        public List<string> Roles { get; set; } = new();

        public bool IsActive { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
        public DateTime? AnonymizedAt { get; set; }
    }
}
