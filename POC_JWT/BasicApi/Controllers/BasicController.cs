
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class BasicController : ControllerBase
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="authService">Service d'authentification</param>
        public BasicController()
        {

        }

        [HttpGet(Name = "home")]
        public IActionResult Home()
        {
            return Ok("Tu est sur la page d'accueil si tu as le bon JWT");
        }



        [Authorize(Roles = "Administrator")]
        [HttpGet(Name = "admin")]
        public IActionResult Admin()
        {
            return Ok("Tu est un admin authorisé.");
        }

       
    }
}
