using RassoApi.DTOs.Responses.Event;
using RassoApi.Models.EventModels;

namespace RassoApi.Mappers
{
    public interface IEventMapper
    {
        EventResponse ToEventResponse(Event ev, bool isFavorite = false);
        DetailedEventResponse ToDetailedEventResponse(Event ev, int participantCount, bool isFavorite = false);
    }
}
