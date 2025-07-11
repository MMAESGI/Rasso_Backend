using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RassoApi.DTOs.Requests.Event;
using RassoApi.DTOs.Responses.Event;
using RassoApi.Exceptions;
using RassoApi.Helpers.Api;
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
            List<EventResponse> events = await _eventService.GetAllEventsAsync();
            return Ok(ApiResponse<List<EventResponse>>.SuccessResponse(events));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<DetailedEventResponse>>> GetById(Guid id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            if (ev == null)
                return NotFound(ApiResponse<EventResponse>.FailureResponse("Event not found"));

            return Ok(ApiResponse<DetailedEventResponse>.SuccessResponse(ev));
        }

        ///[Authorize]
        [HttpPost] // TODO Role organisateur
        public async Task<ActionResult<ApiResponse<EventResponse>>> Create([FromForm] CreateEventRequest request)
        {
            string? email = GetEmailByClaim();
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("Utilisateur non authentifié ou claim email manquant.");
            }

            EventResponse eventResponse = await _eventService.CreateEventAsync(request, email);
            return Ok(ApiResponse<EventResponse>.SuccessResponse(eventResponse, "Event created successfully."));

        }

        //[Authorize]
        [HttpPut("{id}")]
        [HttpPatch("{id}")] //TODO Role organisateur
        public async Task<ActionResult<ApiResponse<EventResponse>>> Update(Guid id, [FromForm] UpdateEventRequest request)
        {
            var updated = await _eventService.UpdateEventAsync(id, request);
            if (updated == null)
                return NotFound(ApiResponse<EventResponse>.FailureResponse("Event not found"));

            return Ok(ApiResponse<EventResponse>.SuccessResponse(updated, "Event updated"));
        }

        ///[Authorize]
        [HttpDelete("{id}")] //TODO Role
        public async Task<ActionResult<ApiResponse<string>>> Delete(Guid id)
        {
            var result = await _eventService.DeleteEventAsync(id);
            if (!result)
                return NotFound(ApiResponse<string>.FailureResponse("Event not found"));

            return Ok(ApiResponse<string>.SuccessResponse("Event deleted"));
        }

        [HttpGet("populaires")]
        public async Task<ActionResult<ApiResponse<List<EventResponse>>>> GetTop()
        {
            var top = await _eventService.GetTopEventsAsync();
            return Ok(ApiResponse<List<EventResponse>>.SuccessResponse(top));
        }

        //[HttpGet("search")]
        //public async Task<ActionResult<ApiResponse<List<EventResponse>>>> Search([FromQuery] string q)
        //{
        //    //var results = await _eventService.SearchEventsAsync(q);
        //    return Ok(ApiResponse<List<EventResponse>>.SuccessResponse(null));
        //}

        //[Authorize]
        //[HttpGet("favourites")]
        //public async Task<ActionResult<ApiResponse<List<EventResponse>>>> GetFavourites()
        //{
        //    string? email = GetEmailByClaim();
        //    if (string.IsNullOrEmpty(email))
        //    {
        //        return Unauthorized("Utilisateur non authentifié ou claim email manquant.");
        //    }
        //    List<EventResponse> favoriteEvents = await _eventService.GetFavouriteEventsAsync(email);
        //    return Ok(ApiResponse<List<EventResponse>>.SuccessResponse(favoriteEvents));
        //}

        [HttpPut("favorite")]
        [HttpPatch("favorite")]
        public async Task<ActionResult<ApiResponse<string>>> ToggleFavorite([FromBody] ToggleFavoriteRequest request)
        {
            try
            {
                string? email = GetEmailByClaim();
                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized("Utilisateur non authentifié ou claim email manquant.");
                }
                EventResponse result = await _eventService.ToggleFavoriteAsync(email, request.EventId);
            }
            catch (EventException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Une erreur est survenue lors de la récupération des événements");
            }
            return Ok(ApiResponse<string>.SuccessResponse("Favorite updated"));
        }


        [HttpGet("favorite")]
        public async Task<ActionResult<ApiResponse<string>>> GetFavorites()
        {
            try
            {
                string? email = GetEmailByClaim();
                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized("Utilisateur non authentifié ou claim email manquant.");
                }
                List<EventResponse> result = await _eventService.GetFavorites(email);
                return Ok(ApiResponse<List<EventResponse>>.SuccessResponse(result));
            }
            catch (EventException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Une erreur est survenue lors de la récupération des événements");
            }
           
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
            try
            {
                string? email = GetEmailByClaim();
                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized("Utilisateur non authentifié ou claim email manquant.");
                }
                EventResponse eventResponse = await _eventService.GetMainEventAsync(email);
                return Ok(ApiResponse<EventResponse>.SuccessResponse(eventResponse));
            }
            catch (EventException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Une erreur est survenue lors de la récupération des événements");
            }
            
        }

        [HttpGet("user_events")]
        public async Task<ActionResult<ApiResponse<List<EventResponse>>>> GetEventsByUser()
        {
            try
            {
                string? email = GetEmailByClaim();
                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized("Utilisateur non authentifié ou claim email manquant.");
                }
                var user = await _eventService.GetUserByEmail(email);
                if (user == null)
                {
                    return NotFound(ApiResponse<List<EventResponse>>.FailureResponse("Utilisateur non trouvé"));
                }

                List<EventResponse> events = await _eventService.GetEventsByUserIdAsync(user.Id);
                return Ok(ApiResponse<List<EventResponse>>.SuccessResponse(events, "Events retrieved successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<List<EventResponse>>.FailureResponse($"Erreur lors de la récupération des événements : {ex.Message}"));
            }
        }

        private string? GetEmailByClaim()
        {
            return User.FindFirst(ClaimTypes.Email)?.Value
                ?? User.FindFirst("email")?.Value;
        }
    }

}
