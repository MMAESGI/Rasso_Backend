using Common.Results;
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
        private readonly IPasswordValidator _passwordValidator;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userRepository">User repository</param>
        public UserService(IUserRepository userRepository, IPasswordValidator passwordValidator)
        {
            _userRepository = userRepository;
            _passwordValidator = passwordValidator;
        }


        /// <inheritdoc />
        public async Task<Result<User>> GetUser(string email, string password)
        {
            User? user = _userRepository.GetByEmail(email);
            if (user != null && user.IsActive)
            {
                IReadOnlyList<string> errors = await _passwordValidator.ValidateAsync(user, password);
                if (errors.Any())
                {
                    return Result<User>.Fail("Invalid username or password", errors.ToList());

                }
                return Result<User>.Ok(user);
            }
            return Result<User>.Fail("Invalid username or password");
        }
    }
}
