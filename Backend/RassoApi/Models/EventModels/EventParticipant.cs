using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RassoApi.Entity;

namespace RassoApi.Models.EventModels
{
    public class EventParticipant
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}
