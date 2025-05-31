using Identity.DTOs.Responses;
using Identity.Models;

namespace Identity.Mappers
{
    public class IdentityMapper : IIdentityMapper
    {

        public DetailedUserResponse ToDetailedUserResponse(User user)
        {
            return new DetailedUserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                Roles = user.UserRoles
                          .Select(ur => ur.Role.Name)
                          .ToList()
            };
        }

        public UserResponse ToUserResponse(User user)
        {
            return new UserResponse
            {
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
