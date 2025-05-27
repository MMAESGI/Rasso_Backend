using RassoApi.DTOs.Responses.Event;

namespace RassoApi.Services.Events.Interfaces
{
    public interface ISearchService
    {
        Task<List<EventResponse>> SearchEventsAsync(string query);
    }
}
