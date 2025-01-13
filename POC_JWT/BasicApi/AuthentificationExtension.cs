using System.Text;
using BasicApi.Configuration;
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
            AppsettingsConfiguration? appsettings = configuration.GetSection("AppSettings").Get<AppsettingsConfiguration>();
            
            if (appsettings is null)
            {
                throw new InvalidOperationException("Le appsettings est invalide ou mal configuré");
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
                    ValidIssuer = appsettings.Jwt.IUsser,
                    ValidAudience = appsettings.Jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appsettings.Jwt.Key))
                };
            });


            return services;
        }
    }
}
