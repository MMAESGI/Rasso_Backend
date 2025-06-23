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
        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetUser(email);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }

}
