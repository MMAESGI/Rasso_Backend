using RassoApi.DTOs.Requests.Event;
using RassoApi.DTOs.Responses.Event;

namespace RassoApi.Services.Events.Interfaces
{
    public interface IEventService
    {
        Task<List<EventResponse>> GetAllEventsAsync();
        Task<DetailedEventResponse> GetEventByIdAsync(Guid id);
        Task<EventResponse> CreateEventAsync(CreateEventRequest request, string username);
        Task<EventResponse> UpdateEventAsync(Guid id, UpdateEventRequest request);
        Task<bool> DeleteEventAsync(Guid id);
        Task<List<EventResponse>> GetTopEventsAsync();
        Task<List<EventResponse>> GetEventsByLocationAsync(string? locationName, double? latitude, double? longitude);
        Task<EventResponse> GetMainEventAsync(string username);

        Task<EventResponse> ToggleFavoriteAsync(string userEmail, Guid EventId);
        Task<List<EventResponse>> GetFavorites(string userEmail);

        Task<RassoApi.DTOs.UserDto?> GetUserByEmail(string email);
        Task<List<EventResponse>> GetEventsByUserIdAsync(Guid userId);

    }
}
