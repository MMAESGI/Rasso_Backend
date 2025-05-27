using RassoApi.Models.EventModels;

namespace RassoApi.Repositories.Interfaces
{
    public interface ISearchRepository
    {
        Task<List<Event>> GetEventBySearchedWord(string query);
    }
}
