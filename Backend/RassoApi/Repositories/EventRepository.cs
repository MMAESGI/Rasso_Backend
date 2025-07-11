using Microsoft.EntityFrameworkCore;
using RassoApi.Database;
using RassoApi.Enums;
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
                .Include(e => e.Images)
                .OrderByDescending(e => e.Date)
                .Take(10)
                .ToListAsync();
        }

        public async Task<List<Event>> GetByLocationAsync(string? locationName, double? latitude, double? longitude)
        {
            var query = _context.Events.Include(e => e.Images).AsQueryable();

            if (!string.IsNullOrEmpty(locationName))
                query = query.Where(e => e.Location!.Contains(locationName));

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
                .Include(e => e.Images)
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

        public async Task<Event> ToggleFavoriteAsync(Guid userId, Event ev)
        {
            Favorite? fav = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.EventId == ev.Id);

            if (fav != null)
            {
                _context.Favorites.Remove(fav);
            }
            else
            {
                _context.Favorites.Add(new Favorite
                {
                    UserId = userId,
                    EventId = ev.Id
                });
            }

            await _context.SaveChangesAsync();
            return await _context.Events.Include(e => e.Images).Where(x => x.Id == ev.Id).FirstOrDefaultAsync();
        }

        public async Task<List<Event>> GetFavorite(Guid userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Event)
                .ThenInclude(e => e.Images)
                .Select(f => f.Event)
                .ToListAsync();
        }

        public async Task<List<Guid>> GetFavoriteEventIds(Guid userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => f.EventId)
                .ToListAsync();
        }



        public async Task<List<Event>> GetPendingEvents()
        {
            return await _context.Events
                .Include(e => e.Status)
                .Where(e => e.Status!.Code == StatusEnum.WAITINGVALIDATION)
                .ToListAsync();
        }



        public async Task<bool> SetEventStatus(Guid eventId, StatusEnum newStatus)
        {
            var ev = await _context.Events.FindAsync(eventId);
            if (ev == null)
                return false;

            var status = await _context.EventStatuses.FirstOrDefaultAsync(s => s.Code == newStatus);
            if (status == null)
                return false;

            ev.StatusId = status.Id;
            ev.ModeratedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddEventMediaAsync(EventMedia eventMedia)
        {
            _context.EventImages.Add(eventMedia);
            await _context.SaveChangesAsync();
        }

        public async Task<List<string>> GetEventImageUrlsAsync(Guid eventId)
        {
            return await _context.EventImages
                .Where(img => img.EventId == eventId)
                .Select(img => img.S3Url)
                .ToListAsync();
        }

    }

}
