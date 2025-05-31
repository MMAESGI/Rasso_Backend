//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace Identity.Controllers
//{
//    [ApiController]
//    [Route("users")]
//    public class UsersController : ControllerBase
//    {
//        private readonly IUserService _userService;

//        public UsersController(IUserService userService)
//        {
//            _userService = userService;
//        }


//        [Authorize]
//        [HttpPut("{id}")]
//        public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateDto dto)
//        {
//            var result = await _userService.UpdateUserAsync(id, dto);
//            return Ok(result);
//        }

//        [Authorize]
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(Guid id)
//        {
//            await _userService.DeleteUserAsync(id);
//            return NoContent();
//        }
//    }

//}
