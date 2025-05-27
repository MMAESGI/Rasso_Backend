using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RassoApi.DTOs.Requests.Event;
using RassoApi.DTOs.Responses.Event;
using RassoApi.Helpers.Api;
using RassoApi.Mappers;
using RassoApi.Models.EventModels;
using RassoApi.Services.Events.Interfaces;

namespace RassoApi.Controllers
{
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<EventResponse>>>> GetAll()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(ApiResponse<List<EventResponse>>.SuccessResponse(events));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<DetailedEventResponse>>> GetById(Guid id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            if (ev == null)
                return NotFound(ApiResponse<EventResponse>.FailureResponse("Event not found"));

            return Ok(ApiResponse<EventResponse>.SuccessResponse(ev));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<EventResponse>>> Create([FromBody] CreateEventRequest request)
        {
            var created = await _eventService.CreateEventAsync(request, User.Identity!.Name!);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, ApiResponse<EventResponse>.SuccessResponse(created, "Event created successfully"));
        }

        [Authorize]
        [HttpPut("{id}")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<ApiResponse<EventResponse>>> Update(Guid id, [FromBody] UpdateEventRequest request)
        {
            var updated = await _eventService.UpdateEventAsync(id, request);
            if (updated == null)
                return NotFound(ApiResponse<EventResponse>.FailureResponse("Event not found"));

            return Ok(ApiResponse<EventResponse>.SuccessResponse(updated, "Event updated"));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(Guid id)
        {
            var result = await _eventService.DeleteEventAsync(id);
            if (!result)
                return NotFound(ApiResponse<string>.FailureResponse("Event not found"));

            return Ok(ApiResponse<string>.SuccessResponse("Event deleted"));
        }

        [HttpGet("top")]
        public async Task<ActionResult<ApiResponse<List<EventResponse>>>> GetTop()
        {
            var top = await _eventService.GetTopEventsAsync();
            return Ok(ApiResponse<List<EventResponse>>.SuccessResponse(top));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ApiResponse<List<EventResponse>>>> Search([FromQuery] string q)
        {
            var results = await _eventService.SearchEventsAsync(q);
            return Ok(ApiResponse<List<EventResponse>>.SuccessResponse(results));
        }

        [Authorize]
        [HttpGet("favourites")]
        public async Task<ActionResult<ApiResponse<List<EventResponse>>>> GetFavourites()
        {
            var favs = await _eventService.GetFavouriteEventsAsync(User.Identity!.Name!);
            return Ok(ApiResponse<List<EventResponse>>.SuccessResponse(favs));
        }

        [Authorize]
        [HttpPut("favorite")]
        [HttpPatch("favorite")]
        public async Task<ActionResult<ApiResponse<string>>> ToggleFavorite([FromBody] ToggleFavoriteRequest request)
        {
            var result = await _eventService.ToggleFavoriteAsync(User.Identity!.Name!, request.EventId);
            return Ok(ApiResponse<string>.SuccessResponse("Favorite updated"));
        }

        [HttpGet("location")]
        public async Task<ActionResult<ApiResponse<List<EventResponse>>>> GetByLocation(
            [FromQuery] string? locationName,
            [FromQuery] double? latitude,
            [FromQuery] double? longitude)
        {
            var events = await _eventService.GetEventsByLocationAsync(locationName, latitude, longitude);
            return Ok(ApiResponse<List<EventResponse>>.SuccessResponse(events));
        }

        [Authorize]
        [HttpGet("main")]
        public async Task<ActionResult<ApiResponse<EventResponse>>> GetMain()
        {
            var main = await _eventService.GetMainEventAsync(User.Identity!.Name!);
            return Ok(ApiResponse<EventResponse>.SuccessResponse(main));
        }
    }

}
