    using Common.Results;
using Identity.Controllers.Requests;
using Identity.DTOs.Responses;
using Identity.Mappers;
using Identity.Models;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [ApiController]
    [Route("api")]
    public class IdentityController : ControllerBase
    {
        IUserService _userService;
        ITokenGenerator _tokenGenerator;
        IIdentityMapper _mapper;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="authService">Service d'authentification</param>
        public IdentityController(IUserService userService, ITokenGenerator tokenGenerator, IIdentityMapper mapper)
        {
            _userService = userService;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
        }

        [HttpPost("auth/connexion")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            Result<User> result = await _userService.GetUser(request.Email, request.Password);
            if (result.Success && result.Value != null)
            {
                UserResponse user  = await _mapper.ToUserResponse(result.Value);
                string token = _tokenGenerator.GenerateToken(result.Value.Id, result.Value.Email);
                return Ok(new { Token = token, User  = user});
            }

            return Unauthorized("Invalid username or password");
        }


        [HttpPost("/auth/inscription")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest)
        {


            Result<User> result = await _userService.RegisterUser(signUpRequest);

            if (!result.Success)
                return BadRequest(result.Error);
            
            DetailedUserResponse userResponse = await _mapper.ToDetailedUserResponse(result.Value!);

            return Ok(userResponse);
        }

    }
}
