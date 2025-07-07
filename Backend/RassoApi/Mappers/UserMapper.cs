using RassoApi.DTOs.Responses.User;

namespace RassoApi.Mappers
{
    public class UserMapper : IUserMapper
    {
        public T ToUserResponse<T>(DTOs.UserDto user) where T : UserResponse, new()
        {
            return new T
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                UserName = user.Username,
                AvatarUrl = user.AvatarUrl,
            };
        }

        public Entity.User ToUserEntity(DTOs.UserDto user)
        {
            return new Entity.User { Id = user.Id, Name = user.LastName + user.FirstName };
        }

        public DTOs.UserDto UserIdentityToUser(Identity.Client.UserDto user)
        {
            return new DTOs.UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                //role = user.Role ,
            };
        }

    }
}
