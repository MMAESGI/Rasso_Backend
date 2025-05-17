using RassoApi.DTOs.Requests.Event;
using RassoApi.Models.Event;

namespace RassoApi.Services.Interfaces.Events
{
    public interface IEventService
    {
        Task<Event> CreateEventAsync(CreateEventRequest request);

        Task<Event> GetEventById(int id);
    }
}
