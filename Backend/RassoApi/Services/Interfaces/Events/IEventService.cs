using RassoApi.DTOs.Requests.Event;
using RassoApi.Models.EventModels;

namespace RassoApi.Services.Interfaces.Events
{
    public interface IEventService
    {
        Task<Event> GetEventById(int id);

        Task<Event> CreateEvent(CreateEventRequest eventRequest);
    }
}
