using Org.BouncyCastle.Crypto;
using RassoApi.DTOs.Requests.Event;
using RassoApi.DTOs.Responses.Event;
using RassoApi.Mappers;
using RassoApi.Models.EventModels;
using RassoApi.Repositories.Interfaces;
using RassoApi.Services.Events.Interfaces;

namespace RassoApi.Services.Events
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepo, IUserRepository userRepo, IMapper mapper)
        {
            _eventRepository = eventRepo;
            _userRepository = userRepo;
            _mapper = mapper;
        }

        public async Task<List<EventResponse>> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAllAsync(includeOrganizer: true);
            return _mapper.Map<List<EventResponse>>(events);
        }

        public async Task<DetailedEventResponse?> GetEventByIdAsync(Guid id)
        {
            var ev = await _eventRepository.GetByIdAsync(id, includeOrganizer: true);
            return ev == null ? null : _mapper.Map<DetailedEventResponse>(ev);
        }

        public async Task<EventResponse> CreateEventAsync(CreateEventRequest request, string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null) throw new Exception("User not found");

            var entity = new Event
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                Date = request.Date,
                OrganizerId = user.Id
            };

            await _eventRepository.AddAsync(entity);
            return _mapper.Map<EventResponse>(entity);
        }

        public async Task<EventResponse?> UpdateEventAsync(Guid id, UpdateEventRequest request)
        {
            var ev = await _eventRepository.GetByIdAsync(id);
            if (ev == null) return null;

            ev.Title = request.Title;
            ev.Description = request.Description;
            ev.Date = request.Date;
            ev.Location = request.Location;

            await _eventRepository.UpdateAsync(ev);
            return _mapper.Map<EventResponse>(ev);
        }

        public async Task<bool> DeleteEventAsync(Guid id)
        {
            return await _eventRepository.DeleteAsync(id);
        }

        public async Task<List<EventResponse>> GetTopEventsAsync()
        {
            var top = await _eventRepository.GetTopEventsAsync();
            return _mapper.Map<List<EventResponse>>(top);
        }

        public async Task<List<EventResponse>> GetEventsByLocationAsync(string? name, double? lat, double? lon)
        {
            var results = await _eventRepository.GetByLocationAsync(name, lat, lon);
            return _mapper.Map<List<EventResponse>>(results);
        }

        public async Task<EventResponse> GetMainEventAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            var ev = await _eventRepository.GetMainEventForUserAsync(user.Id);
            return _mapper.Map<EventResponse>(ev);
        }
    }

}
