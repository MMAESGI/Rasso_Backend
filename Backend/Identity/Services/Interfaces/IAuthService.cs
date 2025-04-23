namespace Identity.Services.Interfaces
{
    /// <summary>
    /// Service d'Authentification d'un utilisateur
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Indique si les identifiants sont valides
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Mot de passe</param>
        /// <returns></returns>
        bool ValidateCredentials(string email, string password);
    }
}
