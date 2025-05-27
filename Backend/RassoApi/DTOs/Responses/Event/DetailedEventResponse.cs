using RassoApi.DTOs.Responses.User;

namespace RassoApi.DTOs.Responses.Event
{
    public class DetailedEventResponse : EventResponse
    {
        public OrganizerResponse Organizer { get; set; } = default!;
        public int ParticipantCount { get; set; }

        public ModeratorResponse? ModeratedByUser { get; set; }
        public DateTime? ModeratedAt { get; set; }
        public string? RefusalReasonLabel { get; set; }
        public string? RefusalComment { get; set; }

        public List<string> ImageUrls { get; set; } = new();
    }
}
