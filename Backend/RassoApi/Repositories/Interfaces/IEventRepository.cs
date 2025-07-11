using RassoApi.Enums;
using RassoApi.Models.EventModels;

namespace RassoApi.Repositories.Interfaces
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync(bool includeImages = false);
        Task<Event?> GetByIdAsync(Guid id, bool includeImages = false);
        Task AddAsync(Event ev);
        Task UpdateAsync(Event ev);
        Task<bool> DeleteAsync(Guid id);
        Task<List<Event>> GetTopEventsAsync();
        Task<List<Event>> GetByLocationAsync(string? locationName, double? latitude, double? longitude);
        Task<Event?> GetMainEventForUserAsync(Guid userId);
        Task<Event> ToggleFavoriteAsync(Guid userId, Event ev);
        Task<List<Event>> GetFavorite(Guid userId);
        Task<List<Guid>> GetFavoriteEventIds(Guid userId);

        Task<List<Event>> GetPendingEvents();
        Task<bool> SetEventStatus(Guid eventId, StatusEnum newStatus);
        Task AddEventMediaAsync(EventMedia eventMedia);
    }
}
