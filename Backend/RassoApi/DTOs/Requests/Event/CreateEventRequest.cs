namespace RassoApi.DTOs.Requests.Event
{
    /// <summary>
    /// Requête pour la création d'événement
    /// </summary>
    public class CreateEventRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
