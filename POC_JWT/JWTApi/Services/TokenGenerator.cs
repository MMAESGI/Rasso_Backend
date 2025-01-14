using JWTApi.Exceptions;
using JWTApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTApi.Services
{

    /// <inheritdoc cref="ITokenGenerator"/>
    public class TokenGenerator : ITokenGenerator
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public TokenGenerator()
        {
        }

        /// <inheritdoc />
        public string GenerateToken(Guid userId, string email)
        {
            // Utiliser pour retourné le token
            JwtSecurityTokenHandler tokenHandler = new();
            string? ApiKey = Environment.GetEnvironmentVariable("ESGI_API_KEY");

            if (String.IsNullOrEmpty(ApiKey))
            {
                throw new InvalidConfigurationException("La clé d'API est vide ou mal configurée.");
            }

            byte[] ByteApiKey = Encoding.UTF8.GetBytes(ApiKey);

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, "User")                                          // A Récup
            };


            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),                                   
                Issuer = "http://id.identity.com",                                              // Celui qui créer le token         
                Audience = "http://baseApi.com",                                           // La "destination" du token, peut varier en fonction du destinataire
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(ByteApiKey), SecurityAlgorithms.HmacSha256)
            };

            SecurityToken token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
