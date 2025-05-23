using System.ComponentModel.DataAnnotations;

namespace RassoApi.Models.EventModels
{
    public class EventStats
    {
        [Key]
        public Guid EventId { get; set; }
        public Event Event { get; set; } = null!;

        public int ParticipantCount { get; set; }
        public string? Category { get; set; }
    }
}
