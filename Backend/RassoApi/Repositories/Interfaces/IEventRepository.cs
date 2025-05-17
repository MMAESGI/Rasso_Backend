using RassoApi.Models.Event;

namespace RassoApi.Repositories.Interfaces
{
    public interface IEventRepository
    {
        Task<Event> AddAsync(Event newEvent);
    }
}
