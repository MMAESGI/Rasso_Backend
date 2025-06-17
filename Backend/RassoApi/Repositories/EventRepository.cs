using Microsoft.EntityFrameworkCore;
using RassoApi.Database;
using RassoApi.Models;
using RassoApi.Models.EventModels;
using RassoApi.Repositories.Interfaces;

namespace RassoApi.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetAllAsync(bool includeImages = false)
        {
            var query = _context.Events.AsQueryable();

            if (includeImages)
                query = query.Include(e => e.Images);

            return await query.ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(Guid id, bool includeImages = false)
        {
            var query = _context.Events.Where(e => e.Id == id);

            if (includeImages)
                query = query.Include(e => e.Images);

            return await query.FirstOrDefaultAsync();
        }

        public async Task AddAsync(Event ev)
        {
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Event ev)
        {
            _context.Events.Update(ev);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null)
                return false;

            _context.Events.Remove(ev);
            
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Event>> GetTopEventsAsync()
        {
            return await _context.Events
                .OrderByDescending(e => e.Date)
                .Take(10)
                .ToListAsync();
        }

        public async Task<List<Event>> GetByLocationAsync(string? name, double? latitude, double? longitude)
        {
            var query = _context.Events.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(e => e.Location!.Contains(name));

            if (latitude.HasValue && longitude.HasValue)
            {
                // Filtrage  autour de la position
                double range = 0.1; // ~10km
                query = query.Where(e =>
                    e.Latitude >= latitude - range && e.Latitude <= latitude + range &&
                    e.Longitude >= longitude - range && e.Longitude <= longitude + range);
            }

            return await query.ToListAsync();
        }

        public async Task<Event?> GetMainEventForUserAsync(Guid userId)
        {
            // Le plus proche event dans le futur pour cet utilisateur
            return await _context.Events
                .Where(e => e.OrganizerId == userId && e.Date > DateTime.UtcNow)
                .OrderBy(e => e.Date)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Event>> GetFavoritesByUserIdAsync(Guid userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Event)
                .Select(f => f.Event)
                .ToListAsync();
        }

        public async Task<bool> ToggleFavoriteAsync(Guid userId, Guid eventId)
        {
            var fav = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.EventId == eventId);

            if (fav != null)
            {
                _context.Favorites.Remove(fav);
            }
            else
            {
                _context.Favorites.Add(new Favorite
                {
                    UserId = userId,
                    EventId = eventId
                });
            }

            await _context.SaveChangesAsync();
            return true;
        }

    }

}
