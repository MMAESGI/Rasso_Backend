namespace Identity.DTOs.Responses
{
    public class DetailedUserResponse : UserResponse
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; } = true; //Temporaire
        public DateTime? CreatedAt { get; set; } = null;
        public DateTime? AnonymizedAt { get; set; }
    }
}
