using Microsoft.EntityFrameworkCore;
using RassoApi.Database;
using RassoApi.Models.EventModels;
using RassoApi.Repositories.Interfaces;

namespace RassoApi.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly AppDbContext _context;
        public SearchRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetEventBySearchedWord(string query)
        {
            return await _context.Events
                .Where(e => e.Title.Contains(query) || 
                           e.Description.Contains(query) || 
                           (e.Location != null && e.Location.Contains(query)) ||
                           (e.Category != null && e.Category.Contains(query)))
                .ToListAsync();
        }
    }
}
