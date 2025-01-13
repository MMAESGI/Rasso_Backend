namespace JWTApi.Services.Interfaces
{
    /// <summary>
    /// Service permettant de générer le token
    /// </summary>
    public interface ITokenGenerator
    {
        /// <summary>
        /// Genère le token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        string GenerateToken(Guid userId, string email);
    }
}
