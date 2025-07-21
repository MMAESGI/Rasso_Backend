
using RassoApi.DTOs;
using RassoApi.DTOs.Requests.Event;
using RassoApi.DTOs.Responses.Event;
using RassoApi.Exceptions;
using RassoApi.Mappers;
using RassoApi.Models.EventModels;
using RassoApi.Repositories.Interfaces;
using RassoApi.Services.Events.Interfaces;
using RassoApi.Services.Storage;


namespace RassoApi.Services.Events
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserProxyService _userProxyService;
        private readonly IEventMapper _eventMapper;
        private readonly IUserMapper _userMapper;
        private readonly IImageStorageService _imageStorageService;

        public EventService(IEventRepository eventRepo,
                            IUserProxyService userProxyService,
                            IEventMapper eventMapper,
                            IUserMapper userMapper,
                            IImageStorageService imageStorageService)
        {
            _eventRepository = eventRepo;
            _userProxyService = userProxyService;
            _eventMapper = eventMapper;
            _userMapper = userMapper;
            _imageStorageService = imageStorageService;
        }

        public async Task<List<EventResponse>> GetAllEventsAsync()
        {
            List<Event> events = await _eventRepository.GetAllAsync(includeImages: true);
            return await _eventMapper.ToEventListResponse(events);
        }

        public async Task<DetailedEventResponse> GetEventByIdAsync(Guid id)
        {
            Event? ev = await _eventRepository.GetByIdAsync(id, includeImages: true);            

            var res = ev == null ? throw new EventException("Evenement non trouvé.") : await _eventMapper.ToDetailedEventResponseAsync(ev);

            return res;
        }

        public async Task<EventResponse> CreateEventAsync(CreateEventRequest request, string email)
        {
            var entity = new Event
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Date = request.Date,
                StatusId = 1,
                Category = request.Category
            };

            try
            {
                UserDto user = await GetUser(email);
                entity.OrganizerId = user.Id;
            } 
            catch
            {
                throw new EventException("Utilisateur non trouvé");
            }

            // Ajouter l'événement
            await _eventRepository.AddAsync(entity);

            // Gestion des images
            if (request.Images != null && request.Images.Any())
            {
                foreach (var image in request.Images)
                {
                    try
                    {
                        var uploadResponse = await _imageStorageService.UploadImageAsync(image);
                        var imageUrl = await _imageStorageService.GetImageUrlAsync(uploadResponse.custom_name);
                        
                        var eventMedia = new EventMedia
                        {
                            Id = Guid.NewGuid(),
                            EventId = entity.Id,
                            S3Url = imageUrl,
                            Filename = uploadResponse.original_name,
                            Description = $"Image pour l'événement {entity.Title}",
                            UploadedAt = DateTime.UtcNow
                        };
                        
                        await _eventRepository.AddEventMediaAsync(eventMedia);
                    }
                    catch (Exception ex)
                    {
                        throw new EventException($"Erreur lors de l'upload de l'image {image.FileName}: {ex.Message}");
                    }
                }
            }

            // Récupérer l'événement avec les images pour la réponse
            var eventWithImages = await _eventRepository.GetByIdAsync(entity.Id, includeImages: true);
            return await _eventMapper.ToEventResponse(eventWithImages ?? entity);
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
            
            if (request.Images != null && request.Images.Any())
            {
                foreach (var image in request.Images)
                {
                    try
                    {
                        var uploadResponse = await _imageStorageService.UploadImageAsync(image);
                        var imageUrl = await _imageStorageService.GetImageUrlAsync(uploadResponse.custom_name);
                        
                        var eventMedia = new EventMedia
                        {
                            Id = Guid.NewGuid(),
                            EventId = ev.Id,
                            S3Url = imageUrl,
                            Filename = uploadResponse.original_name,
                            Description = $"Image pour l'événement {ev.Title}",
                            UploadedAt = DateTime.UtcNow
                        };
                        
                        await _eventRepository.AddEventMediaAsync(eventMedia);
                    }
                    catch (Exception ex)
                    {
                        throw new EventException($"Erreur lors de l'upload de l'image {image.FileName}: {ex.Message}");
                    }
                }
            }

            // Récupérer l'événement avec les images pour la réponse
            var eventWithImages = await _eventRepository.GetByIdAsync(ev.Id, includeImages: true);
            return await _eventMapper.ToEventResponse(eventWithImages ?? ev);
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


        public async Task<UserDto?> GetUserByEmail(string email)
        {
            return await _userProxyService.GetUserByEmail(email);
        }

        public async Task<List<EventResponse>> GetEventsByUserIdAsync(Guid userId)
        {
            var events = await _eventRepository.GetAllAsync(includeImages: true);
            var filtered = events.Where(e => e.OrganizerId == userId).ToList();
            return await _eventMapper.ToEventListResponse(filtered);
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
