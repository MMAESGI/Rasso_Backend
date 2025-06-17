using RassoApi.DTOs;
using RassoApi.DTOs.Responses.User;

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
    }
}
