using Identity.Models;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace Identity.Services
{
    /// <inheritdoc cref="IPasswordManager"/>
    public class PasswordManager : IPasswordManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IPasswordValidator<User> _validator;
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordManager(IPasswordValidator<User> validator, UserManager<User> userManager, IPasswordHasher<User> passwordHasher)
        {
            _userManager = userManager;
            _validator = validator;
            _passwordHasher = passwordHasher;
        }

        public async Task<IReadOnlyList<string>> ValidateAsync(User user, string password)
        {
            IdentityResult result = await _validator.ValidateAsync(_userManager, user, password);
            return result.Succeeded ? Array.Empty<string>() : result.Errors.Select(e => e.Description).ToList();
        }

        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }
    }
}
