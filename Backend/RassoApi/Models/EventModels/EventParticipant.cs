using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RassoApi.Models.EventModels
{
    public class EventParticipant
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid EventId { get; set; }
        public Event Event { get; set; } = null!;

        public Guid UserId { get; set; }

        // User minimal reference - see note ci-dessous
        [NotMapped]
        public object? User { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}
