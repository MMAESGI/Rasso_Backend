using RassoApi.Services.Events;
using RassoApi.Services.Interfaces.Events;

namespace RassoApi.Extensions
{
    public static class DIFavorites
    {
        /// <summary>
        /// Ajoute les services liés aux favoris
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFavoritesServices(this IServiceCollection services)
        {



            return services;
        }
    }
}
