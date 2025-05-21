using System.ComponentModel.DataAnnotations;

namespace RassoApi.Models.EventModels
{
    public class EventMedia
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid EventId { get; set; }
        public Event Event { get; set; } = null!;

        [Required, MaxLength(500)]
        public string S3Url { get; set; } = null!;

        [MaxLength(255)]
        public string? Filename { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
