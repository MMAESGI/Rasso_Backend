using JWTApi.Services.Interfaces;

namespace JWTApi.Services
{
    /// <inheritdoc cref="IAuthService"/>
    public class AuthService : IAuthService
    {
        public AuthService(IUserRepository userRepository)
        {
        }


        /// <inheritdoc />
        public bool ValidateCredentials(string username, string password)
        {
            //TODO
            return true;
        }
    }
}
