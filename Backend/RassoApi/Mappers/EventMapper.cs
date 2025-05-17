using RassoApi.DTOs.Requests.Event;
using RassoApi.DTOs.Responses.Event;
using RassoApi.Models.Event;

namespace RassoApi.Mappers
{
    public static class EventMapper
    {
        public static Event ToEntity(CreateEventRequest dto)
        {
            return new Event
            {
                Title = dto.Title,
                Description = dto.Description,
                Date = dto.Date,
                Location = dto.Location,
                OrganizerId = dto.OrganizerId,
                Category = dto.Category,
                Status = "En attente"
            };
        }

        public static EventResponse ToResponse(Event entity)
        {
            return new EventResponse
            {
                Title = entity.Title,
                Date = entity.Date,
                Location = entity.Location,
                Description = entity.Description,
                OrganizerId = entity.OrganizerId,
                Category = entity.Category,
                Status = entity.Status
            };
        }
    }
}
