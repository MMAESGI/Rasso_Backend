using Identity.DTOs;
using Identity.Models;

namespace Identity.Services
{
    internal static class UserMapper
    {
        public static UserDto ToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
        }
    }
}
