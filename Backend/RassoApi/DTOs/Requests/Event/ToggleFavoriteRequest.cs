namespace RassoApi.DTOs.Requests.Event
{
    /// <summary>
    /// Requête pour (ajouter/supprimer) un événement aux favoris d'un utilisateur
    /// </summary>
    public class ToggleFavoriteRequest
    {
        public Guid EventId { get; set; }
    }
}
