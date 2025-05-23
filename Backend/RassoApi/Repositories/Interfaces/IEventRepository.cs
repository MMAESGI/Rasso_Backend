using RassoApi.Models.EventModels;

namespace RassoApi.Repositories.Interfaces
{
    public interface IEventRepository
    {
        Task<Event> AddAsync(Event newEvent);
    }
}
