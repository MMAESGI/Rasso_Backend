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

        public User ToUserEntity(UserDto user)
        {
            return new User { Id = user.Id, Name = user.LastName + user.FirstName };
        }
    }
}
