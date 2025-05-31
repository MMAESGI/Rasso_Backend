using Identity.DTOs.Responses;
using Identity.Models;

namespace Identity.Mappers
{
    public interface IIdentityMapper
    {
        DetailedUserResponse ToDetailedUserResponse(User user);

        UserResponse ToUserResponse(User user);
    }
}
