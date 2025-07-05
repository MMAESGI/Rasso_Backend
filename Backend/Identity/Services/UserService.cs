using Common.Models;
using Common.Results;
using Identity.Controllers.Requests;
using Identity.Models;
using Identity.Repositories;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Identity.Services
{
    /// <inheritdoc cref="IUserService"/>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordManager _passwordManager;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userRepository">User repository</param>
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IPasswordManager passwordManager, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordManager = passwordManager;
            _roleRepository = roleRepository;
        }


        /// <inheritdoc />
        public async Task<Result<User>> GetUser(string email, string password)
        {
            User? user = _userRepository.GetByEmail(email);
            if (user != null && user.IsActive)
            {
                IReadOnlyList<string> errors = await _passwordManager.ValidateAsync(user, password);
                if (errors.Any())
                {
                    return Result<User>.Fail("Invalid username or password", errors.ToList());

                }
                return Result<User>.Ok(user);
            }
            return Result<User>.Fail("Invalid username or password. Please contact your administrator.");
        }


        public async Task<Result<User>> GetUserByEmail(string email)
        {
            User? user = _userRepository.GetByEmail(email);
            if (user != null && user.IsActive)
            {
                //IReadOnlyList<string> errors = await _passwordManager.ValidateAsync(user, password);
                //if (errors.Any())
                //{
                //    return Result<User>.Fail("Invalid username or password", errors.ToList());

                //}
                return Result<User>.Ok(user);
            }
            return Result<User>.Fail("Invalid username or password. Please contact your administrator.");
        }

        /// <inheritdoc />
        public async Task<Result<User>> RegisterUser(SignUpRequest request)
        {
            if (await _userRepository.UserExistsAsync(request.Email, request.Username))
            {
                return Result<User>.Fail("Registration failed. Please verify your details and try again");
            }

            User user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Username,
                IsActive = true,             // Temporaire           
                CreatedAt = DateTime.UtcNow,
            };

            user.PasswordHash = _passwordManager.HashPassword(user, request.Password);


            Role? defaultRole = await _roleRepository.GetRoleByName(UserRoleEnum.User);
            if (defaultRole == null)
                return Result<User>.Fail("Default role not found.");

            user.UserRoles.Add(new UserRole
            {
                Role = defaultRole,
                User = user
            });



             if (await _userRepository.AddUserAsync(user) > 0)
                return Result<User>.Ok(user);

             return Result<User>.Fail("Registration failed. Please verify your details and try again");
        }
    }
}
