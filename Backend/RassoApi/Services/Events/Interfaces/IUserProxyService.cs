using RassoApi.DTOs;

namespace RassoApi.Services.Events.Interfaces
{
    public interface IUserProxyService
    {
        Task<UserDto?> GetUserByIdAsync(Guid userId);

    }
}
