using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using RassoApi.DTOs.Requests.Event;
using RassoApi.DTOs.Responses.Event;
using RassoApi.Helpers.Api;
using RassoApi.Mappers;
using RassoApi.Models.Event;
using RassoApi.Services.Interfaces.Events;

namespace RassoApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/event")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventRequest request)
        {

            Event createdEvent = await _eventService.CreateEventAsync(request);

            EventResponse eventResponse = EventMapper.ToResponse(createdEvent);


            //Normalement se serait Created() avec status 201 
            return Ok(ApiResponse<EventResponse>.SuccessResponse(eventResponse, "Événement créé avec succès"));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {

            Event createdEvent = await _eventService.GetEventById(id);

            if (createdEvent == null)
            {
                return NotFound(ApiResponse<object>.FailureResponse("Événement non trouvé"));
            }

            EventResponse eventResponse = EventMapper.ToResponse(createdEvent);

            return Ok(ApiResponse<EventResponse>.SuccessResponse(eventResponse));
        }
    }
}
