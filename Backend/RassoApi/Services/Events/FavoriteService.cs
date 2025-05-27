using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using RassoApi.DTOs.Responses.Event;
using RassoApi.Models;
using RassoApi.Repositories.Interfaces;
using RassoApi.Services.Events.Interfaces;

namespace RassoApi.Services.Events
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public FavoriteService(IUserRepository userRepo, IEventRepository eventRepo, IMapper mapper)
        {
            _userRepository = userRepo;
            _eventRepository = eventRepo;
            _mapper = mapper;
        }

        public async Task<List<EventResponse>> GetFavouriteEventsAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            var favs = await _eventRepository.GetFavoritesByUserIdAsync(user.Id);
            return _mapper.Map<List<EventResponse>>(favs);
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
