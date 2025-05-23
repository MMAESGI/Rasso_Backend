using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RassoApi.Models.EventModels
{
    public class EventMedia
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; } = null!;

        // Lien vers le fichier dans S3
        [Required, MaxLength(500)]
        public string S3Url { get; set; } = null!;

        [MaxLength(255)]
        public string? Filename { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }

}
