using RassoApi.DTOs.Responses.Event;
using RassoApi.Models.EventModels;

namespace RassoApi.Services.Events.Interfaces
{
    public interface IFavoriteService
    {
        Task<List<EventResponse>> GetFavouriteEventsAsync(string username);
        Task<bool> ToggleFavoriteAsync(string username, Guid eventId);

    }

}
