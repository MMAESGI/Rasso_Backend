
using RassoApi.DTOs;
using RassoApi.DTOs.Requests.Event;
using RassoApi.DTOs.Responses.Event;
using RassoApi.Exceptions;
using RassoApi.Mappers;
using RassoApi.Models.EventModels;
using RassoApi.Repositories.Interfaces;
using RassoApi.Services.Events.Interfaces;


namespace RassoApi.Services.Events
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserProxyService _userProxyService;
        private readonly IEventMapper _eventMapper;
        private readonly IUserMapper _userMapper;

        public EventService(IEventRepository eventRepo,
                            IUserProxyService userProxyService,
                            IEventMapper eventMapper,
                            IUserMapper userMapper)
        {
            _eventRepository = eventRepo;
            _userProxyService = userProxyService;
            _eventMapper = eventMapper;
            _userMapper = userMapper;
        }

        public async Task<List<EventResponse>> GetAllEventsAsync()
        {
            List<Event> events = await _eventRepository.GetAllAsync(includeImages: true);
            return await _eventMapper.ToEventListResponse(events);
        }

        public async Task<DetailedEventResponse> GetEventByIdAsync(Guid id)
        {
            Event? ev = await _eventRepository.GetByIdAsync(id, includeImages: true);
            return ev == null ? throw new EventException("Evenement non trouvé.") : await _eventMapper.ToDetailedEventResponseAsync(ev);
        }

        public async Task<EventResponse> CreateEventAsync(CreateEventRequest request, string email)
        {

            var entity = new Event
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                Date = request.Date,
                StatusId = 1
            };

            try
            {
                UserDto user = await GetUser(email);
                entity.OrganizerId = user.Id;
            } catch
            {
                //Log.Error("Utilisateur non trouvé");
            }


            await _eventRepository.AddAsync(entity);
            return await _eventMapper.ToEventResponse(entity);
        }

        public async Task<EventResponse> UpdateEventAsync(Guid id, UpdateEventRequest request)
        {
            Event? ev = await _eventRepository.GetByIdAsync(id);
            if (ev == null) throw new EventException("Evénement non trouvé.");

            ev.Title = request.Title;
            ev.Description = request.Description;
            ev.Date = request.Date;
            ev.Location = request.Location;

            await _eventRepository.UpdateAsync(ev);         // TODO la logique de maj est mauvaise, on est pas sur que cela soit modifier
            return await _eventMapper.ToEventResponse(ev);
        }

        public async Task<bool> DeleteEventAsync(Guid id)
        {
            return await _eventRepository.DeleteAsync(id);
        }

        public async Task<List<EventResponse>> GetTopEventsAsync()
        {
            List<Event> top = await _eventRepository.GetTopEventsAsync();
            return await _eventMapper.ToEventListResponse(top);
        }

        public async Task<List<EventResponse>> GetEventsByLocationAsync(string? name, double? latitude, double? longitude)
        {
            List<Event> results = await _eventRepository.GetByLocationAsync(name, latitude, longitude);
            return await _eventMapper.ToEventListResponse(results);
        }

        public async Task<EventResponse> GetMainEventAsync(string email)
        {
            UserDto user = await GetUser(email);
            Event? ev = await _eventRepository.GetMainEventForUserAsync(user.Id);
            if (ev != null)
            {
                return await _eventMapper.ToEventResponse(ev);
            }
            throw new EventException("Evenement non trouvé.") ;
        }

        private async Task<UserDto> GetUser(string email)
        {
            UserDto? user = await _userProxyService.GetUserByEmail(email);
            if (user == null)
            {
                throw new Exception("Utilisateur non trouvé.");
            }
            return user;
        }

        public async Task<EventResponse> ToggleFavoriteAsync(string userEmail, Guid EventId)
        {
            try
            {
                UserDto user = await GetUser(userEmail);
                Event? ev = await _eventRepository.GetByIdAsync(EventId, includeImages: true);
                if (ev != null)
                {
                    Event eventResult = await _eventRepository.ToggleFavoriteAsync(user.Id, ev);
                    return await _eventMapper.ToEventResponse(eventResult);
                }
            }
            catch(Exception ex)
            {
                throw new EventException("Une erreur est survenue lors de la modification de l'événement", ex);
            }
            throw new EventException("Evenement non trouvé");
        }

        public async Task<List<EventResponse>> GetFavorites(string userEmail)
        {
            try
            {
                UserDto user = await GetUser(userEmail);
                List<Event> favoriteEvents = await _eventRepository.GetFavorite(user.Id);

                if (favoriteEvents == null || !favoriteEvents.Any())
                {
                    throw new EventException("Aucun événement favori trouvé pour l'utilisateur.");
                }

                return await _eventMapper.ToEventListResponse(favoriteEvents);
            }
            catch (Exception ex)
            {
                throw new EventException("Une erreur est survenue lors de la récupération des événements favoris.", ex);
            }
        }

    }

}
