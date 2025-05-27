using RassoApi.Enums;

namespace RassoApi.DTOs.Responses.Event
{
    public class EventResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public string? Location { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Category { get; set; }
        public StatusEnum? Status { get; set; }
        public bool IsFavorite { get; set; }

    }
}
