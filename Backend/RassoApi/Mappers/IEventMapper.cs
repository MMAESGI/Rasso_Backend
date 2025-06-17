using RassoApi.DTOs.Responses.Event;
using RassoApi.Models.EventModels;

namespace RassoApi.Mappers
{
    /// <summary>
    /// Interface for mapping Event models to response DTOs.
    /// </summary>
    public interface IEventMapper
    {
        /// <summary>
        /// Maps an Event model to an EventResponse DTO.
        /// </summary>
        /// <param name="ev"></param>
        /// <param name="isFavorite"></param>
        /// <returns></returns>
        EventResponse ToEventResponse(Event ev, bool isFavorite = false);

        /// <summary>
        /// Maps an Event model to a DetailedEventResponse DTO.
        /// </summary>
        /// <param name="ev"></param>
        /// <param name="isFavorite"></param>
        /// <param name="participantCount"></param>
        /// <returns></returns>
        Task<DetailedEventResponse> ToDetailedEventResponseAsync(Event ev,
                                                      bool isFavorite = false,
                                                      int participantCount = 0);
    }
}
