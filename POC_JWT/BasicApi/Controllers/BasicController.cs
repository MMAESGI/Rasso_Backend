
using BasicApi.Entity;
using BasicApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class BasicController : ControllerBase
    {
        private readonly IMySqlService _mySqlService;

        /// <summary>
        /// Ctor
        /// </summary>
        public BasicController(IMySqlService sqlService)
        {
            _mySqlService = sqlService;
        }

        
        [HttpGet("home")]
        public IActionResult Home()
        {
            List<User> users = _mySqlService.GetUsers();
            return Ok(users);
        }



        [Authorize(Roles = "Administrator")]
        [HttpGet("admin")]
        public IActionResult Admin()
        {
            return Ok("Tu est un admin authorisé.");
        }

       
    }
}
