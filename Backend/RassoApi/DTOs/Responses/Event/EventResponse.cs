namespace RassoApi.DTOs.Responses.Event
{
    public class EventResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int OrganizerId { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }

    }
}
