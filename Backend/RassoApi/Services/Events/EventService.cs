using RassoApi.DTOs.Requests.Event;
using RassoApi.Mappers;
using RassoApi.Models.EventModels;
using RassoApi.Repositories.Interfaces;
using RassoApi.Services.Interfaces.Events;

namespace RassoApi.Services.Events
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public Task<Event> CreateEvent(CreateEventRequest eventRequest)
        {
            throw new NotImplementedException();
        }


        async Task<Event> IEventService.GetEventById(int id)
        {
            Event result = new();

            return await Task.FromResult(result);
        }
    }
}
