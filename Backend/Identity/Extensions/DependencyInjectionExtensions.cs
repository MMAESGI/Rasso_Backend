using Identity.Mappers;
using Identity.Repositories;
using Identity.Services;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Identity.Extensions
{
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Méthode d'extension permettant d'enregistrer les service de l'application
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            // Base de données
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            

            // Services
            services.AddScoped<IIdentityMapper, IdentityMapper>();
            services.AddScoped<IPasswordManager, PasswordManager>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IUserService, UserService>();


            return services;
        }

        /// <summary>
        /// Services pour la gestion de l'identité, role et utilisateur
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TRole"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddIdentityServices<TContext, TUser, TRole>(this IServiceCollection services)
            where TContext : DbContext
            where TUser : IdentityUser<Guid>
            where TRole : IdentityRole<Guid>
        {
            services.AddIdentity<TUser, TRole>(IdentityOptionsExtension.BuildIdentityOptions)
            .AddEntityFrameworkStores<TContext>()
            .AddRoleManager<RoleManager<TRole>>()
            .AddUserManager<UserManager<TUser>>()
            .AddSignInManager<SignInManager<TUser>>()
            .AddDefaultTokenProviders();

            return services;
        }

    }
}
