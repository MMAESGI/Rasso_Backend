using RassoApi.DTOs.Requests.Event;
using RassoApi.DTOs.Responses.Event;
using RassoApi.Models.EventModels;
using RassoApi.Services.Events.Interfaces;

namespace RassoApi.Mappers
{
    public static class EventMapper
    {
        public static EventResponse ToResponse(Event ev)
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
            };
        }



        public static async Task<DetailedEventResponse> ToDetailedResponseAsync(
                            Event ev,
                            IUserProxyService userProxyService,
                            bool isFavorite,
                            int participantCount)
        {
            var organizerTask = userProxyService.GetUserByIdAsync(ev.OrganizerId);
            Task<ModeratorResponse?> moderatedTask = Task.FromResult<ModeratorResponse?>(null);

            if (ev.ModeratedByUserId.HasValue)
                moderatedTask = userProxyService.GetUserByIdAsync(ev.ModeratedByUserId.Value);

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
                IsFavorite = isFavorite,
                Organizer = organizerTask.Result!,
                ModeratedByUser = moderatedTask.Result,
                ModeratedAt = ev.ModeratedAt,
                RefusalReasonLabel = ev.RefusalReason?.Label,
                RefusalComment = ev.RefusalComment,
                ParticipantCount = participantCount,
                ImageUrls = ev.Images?.Select(i => i.S3Url).ToList() ?? new List<string>()
            };
        }

    }

}
