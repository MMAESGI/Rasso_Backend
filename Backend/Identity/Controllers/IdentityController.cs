using Common.Results;
using Identity.Controllers.Requests;
using Identity.DTOs.Responses;
using Identity.Mappers;
using Identity.Models;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [ApiController]
    [Route("api")]
    public class IdentityController : ControllerBase
    {
        IUserService _authService;
        ITokenGenerator _tokenGenerator;
        IIdentityMapper _mapper;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="authService">Service d'authentification</param>
        public IdentityController(IUserService authService, ITokenGenerator tokenGenerator, IIdentityMapper mapper)
        {
            _authService = authService;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            Result<User> result = await _authService.GetUser(request.Email, request.Password);
            if (result.Success && result.Value != null)
            {
                UserResponse user  = _mapper.ToUserResponse(result.Value);
                string token = _tokenGenerator.GenerateToken(result.Value.Id, result.Value.Email);
                return Ok(new { Token = token, User  = user});
            }

            return Unauthorized("Invalid username or password");
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest)
        {
            Result<User> result = await _userService.SignUpAsync(signUpRequest);

            if (result.Success)
            {
                return Ok(result);
            }
            return 
        }

    }
}
