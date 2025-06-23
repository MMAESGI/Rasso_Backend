using Microsoft.AspNetCore.Identity;

namespace Identity.Extensions
{
    public static class IdentityOptionsExtension
    {
        public static void BuildIdentityOptions(this IdentityOptions options)
        {
            // Options mot de passe
            options.Password.RequireDigit = true;            // Exige un chiffre
            options.Password.RequireLowercase = true;        // Exige une lettre minuscule
            options.Password.RequireUppercase = true;        // Exige une lettre majuscule
            options.Password.RequireNonAlphanumeric = false; // Exige un caractère spécial
            options.Password.RequiredLength = 8;             // Longueur minimum du mot de passe
            options.Password.RequiredUniqueChars = 1;        // Nombre de caractères uniques requis

            // Options verrouillage (lockout)
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);  // Durée du verrouillage
            options.Lockout.MaxFailedAccessAttempts = 5;      // Nombre d’essais avant verrouillage
            options.Lockout.AllowedForNewUsers = true;        // Verrouiller les nouveaux utilisateurs ?

            // Options utilisateur
            options.User.RequireUniqueEmail = true;

            // Options de connexion
            options.SignIn.RequireConfirmedEmail = false;     // Demander la confirmation email avant connexion
            options.SignIn.RequireConfirmedPhoneNumber = false;
        }
    }
}
