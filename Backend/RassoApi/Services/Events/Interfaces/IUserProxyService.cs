using RassoApi.DTOs;

namespace RassoApi.Services.Events.Interfaces
{
    public interface IUserProxyService
    {
        Task<UserDto?> GetUserByIdAsync(Guid userId);
        Task<List<UserDto?>> GetUsersByIdsAsync(List<Guid> userIds);
        Task<UserDto?> GetUserByEmail(string email);

    }
}
