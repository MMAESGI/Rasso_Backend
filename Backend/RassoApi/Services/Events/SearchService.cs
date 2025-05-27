using Microsoft.EntityFrameworkCore;
using RassoApi.Database;
using RassoApi.DTOs.Responses.Event;
using RassoApi.Models.EventModels;
using RassoApi.Repositories.Interfaces;
using RassoApi.Services.Events.Interfaces;

namespace RassoApi.Services.Events
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;

        public SearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public async Task<List<EventResponse>> SearchEventsAsync(string query)
        {
            List<Event> events = await _searchRepository.GetEventBySearchedWord(query);

            if (events == null || !events.Any())
            {
                return new List<EventResponse>();
            }


        }
    }
}
