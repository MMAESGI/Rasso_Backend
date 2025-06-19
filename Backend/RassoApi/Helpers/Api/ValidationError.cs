namespace RassoApi.Helpers.Api
{
    /// <summary>
    /// Erreur pour la validation des objets
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Champ en erreur
        /// </summary>
        public string Field { get; set; } = string.Empty;

        /// <summary>
        /// Liste des erreurs
        /// </summary>
        public List<string> Errors { get; set; } = new();
    }
}
