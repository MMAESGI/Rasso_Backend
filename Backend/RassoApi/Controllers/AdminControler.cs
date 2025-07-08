using Microsoft.AspNetCore.Mvc;
using RassoApi.DTOs.Responses.Event;
using RassoApi.Enums;
using RassoApi.Mappers;
using RassoApi.Models.EventModels;
using RassoApi.Repositories.Interfaces;

namespace RassoApi.Controllers
{
    namespace RassoApi.Controllers
    {
        [ApiController]
        [Route("admin/events")]
        public class AdminController : ControllerBase
        {
            private readonly IEventRepository _eventRepository;
            private readonly IEventMapper _eventMapper;

            public AdminController(IEventRepository eventRepository, IEventMapper eventMapper)
            {
                _eventRepository = eventRepository;
                _eventMapper = eventMapper;
            }

            /// <summary>
            /// Récupère tous les événements en attente de validation
            /// </summary>
            [HttpGet("pending")]
            public async Task<ActionResult<List<EventResponse>>> GetPendingEvents()
            {
                var pendingEvents = await _eventRepository.GetPendingEvents();
                var response = await _eventMapper.ToEventListResponse(pendingEvents);
                return Ok(response);
            }

            /// <summary>
            /// Valide un événement
            /// </summary>
            [HttpPost("{eventId}/validate")]
            public async Task<IActionResult> ValidateEvent(Guid eventId)
            {
                var success = await _eventRepository.SetEventStatus(eventId, StatusEnum.VALIDATED);
                if (!success)
                    return NotFound($"Événement {eventId} non trouvé ou déjà validé.");

                return NoContent();
            }

            /// <summary>
            /// Refuse un événement
            /// </summary>
            [HttpPost("{eventId}/reject")]
            public async Task<IActionResult> RejectEvent(Guid eventId)
            {
                var success = await _eventRepository.SetEventStatus(eventId, StatusEnum.REFUSED);
                if (!success)
                    return NotFound($"Événement {eventId} non trouvé ou déjà rejeté.");

                return NoContent();
            }
        }

    }
}
