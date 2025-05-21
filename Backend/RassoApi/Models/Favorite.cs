using System.ComponentModel.DataAnnotations.Schema;
using RassoApi.Models.EventModels;

namespace RassoApi.Models
{
    public class Favorite
    {
        public Guid UserId { get; set; }

        // User minimal reference - see note ci-dessous
        [NotMapped]
        public object? User { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
