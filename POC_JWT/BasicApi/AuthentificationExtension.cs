using System.Text;
using BasicApi.Configuration;
using BasicApi.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BasicApi
{
    public static class AuthentificationExtension
    {
        /// <summary>
        /// Configure l'authentification
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomAuthentification(this IServiceCollection services, IConfiguration configuration)
        {
            AppsettingsConfiguration? appsettings = configuration.GetSection("Configuration").Get<AppsettingsConfiguration>();
            
            if (appsettings is null)
            {
                throw new InvalidConfigurationException("Le appsettings est invalide ou mal configuré.");
            }

            string? ApiKey = Environment.GetEnvironmentVariable("API_KEY");

            if (String.IsNullOrEmpty(ApiKey))
            {
                throw new InvalidConfigurationException("La clé d'API est vide ou mal configurée.");
            }

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {

                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = appsettings.Jwt.IUsser,
                    ValidAudience = appsettings.Jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ApiKey))
                };
            });


            return services;
        }
    }
}
