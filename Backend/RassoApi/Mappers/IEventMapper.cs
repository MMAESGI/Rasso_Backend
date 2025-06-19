using RassoApi.DTOs.Responses.Event;
using RassoApi.Models.EventModels;

namespace RassoApi.Mappers
{
    /// <summary>
    /// Interface for mapping Event models to response.
    /// </summary>
    public interface IEventMapper
    {
        /// <summary>
        /// Maps an Event model to an EventResponse.
        /// </summary>
        /// <param name="ev"></param>
        /// <param name="isFavorite"></param>
        /// <returns></returns>
        EventResponse ToEventResponse(Event ev);

        /// <summary>
        ///  Maps an Event model to an EventResponse.
        /// </summary>
        /// <param name="ev"></param>
        /// <param name="isFavorite"></param>
        /// <returns></returns>
        List<EventResponse> ToEventListResponse(List<Event> ev);

        /// <summary>
        /// Maps an Event model to a DetailedEventResponse.
        /// </summary>
        /// </summary>
        /// <param name="ev"></param>
        /// <param name="isFavorite"></param>
        /// <param name="participantCount"></param>
        /// <returns></returns>
        Task<DetailedEventResponse> ToDetailedEventResponseAsync(Event ev);

        /// <summary>
        /// Maps an Event model to a DetailedEventResponse.
        /// </summary>
        /// <param name="ev"></param>
        /// <param name="isFavorite"></param>
        /// <param name="participantCount"></param>
        /// <returns></returns>
        Task<List<DetailedEventResponse>> ToDetailedEventListResponseAsync(List<Event> ev);
    }
}
