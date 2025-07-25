﻿using Common.Models;
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
        private readonly IPasswordManager _passwordManager;
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userRepository">User repository</param>
        public UserService(IPasswordManager passwordManager,
                           IPasswordHasher<User> passwordHasher,
                           UserManager<User> userManager)
        {
            _passwordManager = passwordManager;
            _userManager = userManager;
        }

        public List<User> GetAll()
        {
            return _userManager.Users.ToList();
        }

        public async Task<Result<User>> GetById(Guid id)
        {
            User? user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return Result<User>.Fail("Cannot Find User");
            }
            return Result<User>.Ok(user);
        }

        /// <inheritdoc />
        public async Task<Result<User>> GetUser(string email, string password)
        {
            User? user = await _userManager.FindByEmailAsync(email);
            if (user != null && user.IsActive)
            {
                bool isPasswordValid = _passwordManager.VerifyPassword(user, password);
                if (!isPasswordValid)
                {
                    return Result<User>.Fail("Invalid username or password");
                }
                return Result<User>.Ok(user);
            }
            return Result<User>.Fail("Invalid username or password. Please contact your administrator.");
        }


        public async Task<Result<User>> GetUserByEmail(string email)
        {
            User? user = await _userManager.FindByEmailAsync(email);
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
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return Result<User>.Fail("This email is already registered. Please use a different email.");
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

            IdentityResult identityResult = await _userManager.CreateAsync(user, request.Password);

            if (!identityResult.Succeeded)
            {
                return Result<User>.Fail($"User creation errors: {string.Join(", ", identityResult.Errors.Select(e => e.Description))}");
            }

            if (identityResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin"); // Temporaire
                return Result<User>.Ok(user);
            }

            return Result<User>.Fail("Registration failed. Please verify your details and try again");
        }
    }
}
