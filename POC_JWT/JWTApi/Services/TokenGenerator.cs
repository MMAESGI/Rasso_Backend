using JWTApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            byte[] key = "MySecretKeyThatShouldNotBeHere"u8.ToArray();                      // A sauvegarder de manière sécurisé, pas en dure dans le code

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
                Issuer = "https://id.poc.com",                                              // Celui qui créer le token
                Audience = "https://mainApp.com",                                           // La "destination" du token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            SecurityToken token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
