using Common.Models;
using Identity.Client;
using RassoApi.DTOs;
using RassoApi.DTOs.Responses.User;
using RassoApi.Entity;

namespace RassoApi.Mappers
{
    public class UserMapper : IUserMapper
    {
        public T ToUserResponse<T>(UserDto user) where T : UserResponse, new()
        {
            return new T
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                UserName = user.Username,
                AvatarUrl = user.AvatarUrl,
            };
        }

        public Entity.User ToUserEntity(UserDto user)
        {
            return new Entity.User { Id = user.Id, Name = user.LastName + user.FirstName };
        }

        public UserDto UserIdentityToUser(Identity.Client.User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
                //role = user.UserRoles
            };
        }

    }
}
