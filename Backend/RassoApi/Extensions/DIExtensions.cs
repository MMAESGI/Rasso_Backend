namespace RassoApi.Extensions
{
    public static class DIExtensions
    {
        /// <summary>
        /// Méthode d'extension permettant d'enregistrer les service de l'application
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddEventServices();
            services.AddFavoritesServices();


            return services;
        }



    }
}
