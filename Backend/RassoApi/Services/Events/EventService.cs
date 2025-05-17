using RassoApi.DTOs.Requests.Event;
using RassoApi.Mappers;
using RassoApi.Models.Event;
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

        public async Task<Event> CreateEventAsync(CreateEventRequest request)
        {
            // Ici tu pourrais ajouter des règles métier (ex: validation, vérification droits, etc.)
            Event newEvent = EventMapper.ToEntity(request);



            // Ajouter en base
            var createdEvent = await _eventRepository.AddAsync(newEvent);

            return createdEvent;
        }

        public async Task<Event> GetEventById(int id)
        {
            Event result = new();

            return result;
        }
    }
}
