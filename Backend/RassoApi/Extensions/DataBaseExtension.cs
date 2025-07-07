using RassoApi.Repositories;
using RassoApi.Repositories.Interfaces;

using RassoApi.Services.Events;
using RassoApi.Services.Events.Interfaces;

namespace RassoApi.Extensions
{
    public static class DataBaseExtension
    {
        /// <summary>
        /// Ajoute tous les services liés à la base de données
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDataBaseServices(this IServiceCollection services)
        {
            _ = services.AddScoped<IEventRepository, EventRepository>();
            _ = services.AddSingleton<IEventValidator, EventValidator>();
            return services;
        }
    }
}
