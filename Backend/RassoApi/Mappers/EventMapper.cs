using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RassoApi.DTOs;
using RassoApi.DTOs.Responses.Event;
using RassoApi.DTOs.Responses.User;
using RassoApi.Models.EventModels;
using RassoApi.Services.Events.Interfaces;

namespace RassoApi.Mappers
{
    public class EventMapper : IEventMapper
    {
        private readonly IUserProxyService _userProxyService;
        private readonly IUserMapper _userMapper;
        public EventMapper(IUserProxyService userProxyService, IUserMapper userMapper)
        {
            _userProxyService = userProxyService;
            _userMapper = userMapper;
        }

        public EventResponse ToEventResponse(Event ev)
        {
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
                IsFavorite = false, // TODO: Implement favorite logic
            };
        }

        public List<EventResponse> ToEventListResponse(List<Event> ev)
        {
            List<EventResponse> responses = new();

            foreach (var eventEntity in ev)
            {
                responses.Add(ToEventResponse(eventEntity));
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
    }

}
