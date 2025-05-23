using RassoApi.Enums;

namespace RassoApi.DTOs.Responses.Event
{
    public class EventResponse
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public string? Location { get; set; }

        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        //public GUID OrganizerId { get; set; }     user qui organise
        public string? Category { get; set; }
        public StatusEnum? Status { get; set; }

    }
}
