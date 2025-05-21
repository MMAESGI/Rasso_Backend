using RassoApi.Models.EventModels;
using RassoApi.Repositories.Interfaces;

namespace RassoApi.Repositories
{
    public class EventRepository : IEventRepository
    {
        public Task<Event> AddAsync(Event newEvent)
        {
            throw new NotImplementedException();
        }
    }
}
