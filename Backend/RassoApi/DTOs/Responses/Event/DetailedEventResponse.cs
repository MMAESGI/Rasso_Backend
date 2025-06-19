using RassoApi.DTOs.Responses.User;

namespace RassoApi.DTOs.Responses.Event
{
    public class DetailedEventResponse : EventResponse
    {
        /// <summary>
        /// Organisateur de l'événement.
        /// </summary>
        public OrganizerResponse? Organizer { get; set; } = default!;

        /// <summary>
        /// Nombre de participants inscrits à l'événement.
        /// </summary>
        public int ParticipantCount { get; set; }

        /// <summary>
        /// Administrateur qui a modéré l'événement.
        /// </summary>
        public ModeratorResponse? ModeratedByUser { get; set; }

        /// <summary>
        /// Date et heure de la modération de l'événement.
        /// </summary>
        public DateTime? ModeratedAt { get; set; }

        /// <summary>
        /// Raison du refus de l'événement.
        /// </summary>
        public string? RefusalReasonLabel { get; set; }

        /// <summary>
        /// Commentaire laissé lors du refus de l'événement.
        /// </summary>
        public string? RefusalComment { get; set; }

        /// <summary>
        /// Liste des images associées à l'événement.
        /// </summary>
        public List<string> ImageUrls { get; set; } = new();
    }
}
