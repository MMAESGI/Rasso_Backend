using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using RassoApi.Database;
using RassoApi.DTOs;
using RassoApi.DTOs.Responses.Event;
using RassoApi.DTOs.Responses.User;
using RassoApi.Models.EventModels;
using RassoApi.Repositories.Interfaces;
using RassoApi.Services.Events.Interfaces;

namespace RassoApi.Mappers
{
    public class EventMapper : IEventMapper
    {
        private readonly IUserProxyService _userProxyService;
        private readonly IEventRepository _eventRepository;
        private readonly IUserMapper _userMapper;
        private readonly AppDbContext _context;

        public EventMapper(IUserProxyService userProxyService, IUserMapper userMapper, AppDbContext context, IEventRepository eventRepository)
        {
            _userProxyService = userProxyService;
            _userMapper = userMapper;
            _context = context;
            _eventRepository = eventRepository;
        }

        public async Task<EventResponse> ToEventResponse(Event ev, Guid? userId = null)
        {
            bool isFavorite = await IsFavorite(ev.Id, userId);

            return new EventResponse
            {
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                Date = ev.Date,
                Location = ev.Location,
                Latitude = ev.Latitude,
                Longitude = ev.Longitude,
                Category = ev.Category,
                IsFavorite = isFavorite
            };
        }

        public async Task<List<EventResponse>> ToEventListResponse(List<Event> ev, Guid? userId = null)
        {
            var responses = new List<EventResponse>();

            // Récupère les IDs des événements favoris de l'utilisateur en une seule fois
            List<Guid> favoriteEventIds = userId.HasValue
                ? await _eventRepository.GetFavoriteEventIds(userId.Value) 
            : new List<Guid>();

            foreach (Event evt in ev)
            {
                responses.Add(new EventResponse
                {
                    Id = evt.Id,
                    Title = evt.Title,
                    Description = evt.Description,
                    Date = evt.Date,
                    Location = evt.Location,
                    Latitude = evt.Latitude,
                    Longitude = evt.Longitude,
                    Category = evt.Category,
                    IsFavorite = favoriteEventIds.Contains(evt.Id)
                });
            }

            return responses;
        }


        public async Task<DetailedEventResponse> ToDetailedEventResponseAsync(Event ev)
        {
            Task<OrganizerResponse?> organizerTask = GetUserResponseAsync<OrganizerResponse>(ev.OrganizerId);
            Task<ModeratorResponse?> moderatedTask = GetUserResponseAsync<ModeratorResponse>(ev.ModeratedByUserId);
            bool isFavorite = false; // TODO
            int participantCount = 0; // TODO

            await Task.WhenAll(organizerTask, moderatedTask);

            return new DetailedEventResponse
            {
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                Date = ev.Date,
                Location = ev.Location,
                Latitude = ev.Latitude,
                Longitude = ev.Longitude,
                Category = ev.Category,
                Status = ev.Status?.Code,
                IsFavorite = isFavorite,                // TODO
                Organizer = organizerTask.Result,
                ModeratedByUser = moderatedTask.Result,
                ModeratedAt = ev.ModeratedAt,
                RefusalReasonLabel = ev.RefusalReason?.Label,
                RefusalComment = ev.RefusalComment,
                ParticipantCount = participantCount,
                ImageUrls = ev.Images?.Select(i => i.S3Url).ToList() ?? new List<string>()
            };
        }

        private async Task<T?> GetUserResponseAsync<T>(Guid? userId) where T : UserResponse, new()
        {
            if (!userId.HasValue)
                return null;

            UserDto? userDto = await _userProxyService.GetUserByIdAsync(userId.Value);
            if (userDto == null)
                return null;

            return _userMapper.ToUserResponse<T>(userDto);
        }

        

        public Task<List<DetailedEventResponse>> ToDetailedEventListResponseAsync(List<Event> ev)
        {
            // Refacto la récupération de la liste des utilisateurs pour l'optimisation
            throw new NotImplementedException();
        }

        private async Task<bool> IsFavorite(Guid eventId, Guid? userId)
        {
            if (!userId.HasValue) return false;

            return await _context.Favorites
                .AnyAsync(f => f.UserId == userId && f.EventId == eventId);
        }
    }

}
