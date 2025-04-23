using Identity.Controllers.Requests;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [ApiController]
    [Route("api")]
    public class IdentityController : ControllerBase
    {
        IAuthService _authService;
        ITokenGenerator _tokenGenerator;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="authService">Service d'authentification</param>
        public IdentityController(IAuthService authService, ITokenGenerator tokenGenerator)
        {
            _authService = authService;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (_authService.ValidateCredentials(request.Email, request.Password))
            {
                Guid userId = Guid.NewGuid(); // trouver l'id par email ?
                string token = _tokenGenerator.GenerateToken(userId, request.Email);
                return Ok(token);
            }
            return Unauthorized("Invalid username or password");
        }

        //[Authorize(Roles = "Administrator")]
    }
}
