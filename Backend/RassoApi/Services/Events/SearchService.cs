using Microsoft.EntityFrameworkCore;
using RassoApi.Database;
using RassoApi.DTOs.Responses.Event;
using RassoApi.Mappers;
using RassoApi.Models.EventModels;
using RassoApi.Repositories.Interfaces;
using RassoApi.Services.Events.Interfaces;

namespace RassoApi.Services.Events
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IEventMapper _eventMapper;

        public SearchService(ISearchRepository searchRepository, IEventMapper eventMapper)
        {
            _searchRepository = searchRepository;
            _eventMapper = eventMapper;
        }

        public async Task<List<EventResponse>> SearchEventsAsync(string query)
        {
            List<Event> events = await _searchRepository.GetEventBySearchedWord(query);

            if (events == null || !events.Any())
            {
                return new List<EventResponse>();
            }
            return _eventMapper.ToEventListResponse(events);

        }
    }
}
