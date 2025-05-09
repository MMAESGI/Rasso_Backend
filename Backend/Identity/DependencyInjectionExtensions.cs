﻿using Identity.Services;
using Identity.Services.Interfaces;

namespace Identity
{
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Méthode d'extension permettant d'enregistrer les service de l'application
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Token
            services.AddSingleton<ITokenGenerator, TokenGenerator>();

            // Authentification
            services.AddSingleton<IAuthService, AuthService>();

            // Base de données
            services.AddSingleton<IUserRepository, UserRepository>();
         

            return services;
        }
    }
}
