using RassoApi.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RassoApi.Models.EventModels
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(255)]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(255)]
        public string? Location { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [MaxLength(50)]
        public string? Category { get; set; }

        public Guid OrganizerId { get; set; }

        [ForeignKey(nameof(OrganizerId))]
        public User? Organizer { get; set; } = null;

        public int StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public EventStatus? Status { get; set; }

        public Guid? ModeratedById { get; set; }

        [ForeignKey(nameof(ModeratedById))]
        public User? ModeratedBy { get; set; }

        public DateTime? ModeratedAt { get; set; }
        public int? RefusalReasonId { get; set; }

        [ForeignKey(nameof(RefusalReasonId))]
        public RefusalReason? RefusalReason { get; set; }

        public string? RefusalComment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<EventMedia> Images { get; set; } = new List<EventMedia>();
    }

}
