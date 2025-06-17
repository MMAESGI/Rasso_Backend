using RassoApi.DTOs;
using RassoApi.DTOs.Responses.User;

namespace RassoApi.Mappers
{
    public interface IUserMapper
    {
        /// <summary>
        /// Maps a User model to a UserResponse DTO.
        /// </summary>
        /// <param name="user">The user model to map.</param>
        /// <returns>A UserResponse DTO containing the user's information.</returns>
        T ToUserResponse<T>(UserDto user) where T : UserResponse, new();
    }
}
