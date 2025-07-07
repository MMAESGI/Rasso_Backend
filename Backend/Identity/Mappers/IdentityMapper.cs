using Identity.DTOs.Responses;
using Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Mappers
{
    public class IdentityMapper : IIdentityMapper
    {
        private readonly UserManager<User> _userManager;
        public IdentityMapper(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<DetailedUserResponse> ToDetailedUserResponse(User user)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);

            return new DetailedUserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                Roles = roles.ToList()
            };
        }

        public async Task<UserResponse> ToUserResponse(User user)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);

            return new UserResponse
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = roles.ToList()
            };
        }
    }
}
