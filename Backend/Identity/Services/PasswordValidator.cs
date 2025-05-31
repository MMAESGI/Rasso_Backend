using Identity.Models;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Identity.Services
{
    public class PasswordValidator : IPasswordValidator
    {
        private readonly UserManager<User> _userManager;
        private readonly IPasswordValidator<User> _validator;
        public PasswordValidator(IPasswordValidator<User> validator, UserManager<User> userManager)
        {
            _userManager = userManager;
            _validator = validator;
        }

        public async Task<IReadOnlyList<string>> ValidateAsync(User user, string password)
        {
            IdentityResult result = await _validator.ValidateAsync(_userManager, user, password);
            return result.Succeeded ? Array.Empty<string>() : result.Errors.Select(e => e.Description).ToList();
        }
    }
}
