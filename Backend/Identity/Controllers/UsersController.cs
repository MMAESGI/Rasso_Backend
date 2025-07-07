using Common.Results;
using Identity.DTOs;
using Identity.Models;
using Identity.Services;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [ApiController] // TODO
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        //[Authorize]
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateDto dto)
        //{
        //    var result = await _userService.UpdateUserAsync(id, dto);
        //    return Ok(result);
        //}

        //[Authorize]
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    await _userService.DeleteUserAsync(id);
        //    return NoContent();
        //}


        [AllowAnonymous]
        [HttpGet("email/{email}")]
        public async Task<UserDto?> GetByEmail(string email)
        {
            Result<User> result = await _userService.GetUserByEmail(email);
            if (result.Success && result.Value != null)
                return UserMapper.ToDto(result.Value);

            return null;
        }

        [AllowAnonymous]
        [HttpGet("id/{id}")]
        public async Task<UserDto?> GetById(Guid id)
        {
            Result<User> result = await _userService.GetById(id);
            if (result.Success && result.Value != null)
                return UserMapper.ToDto(result.Value);

            return null;
        }
    }

}
