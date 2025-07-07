using Identity.DTOs.Responses;
using Identity.Models;

namespace Identity.Mappers
{
    public interface IIdentityMapper
    {
        Task<DetailedUserResponse> ToDetailedUserResponse(User user);

        Task<UserResponse> ToUserResponse(User user);
    }
}
