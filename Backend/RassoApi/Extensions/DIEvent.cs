using RassoApi.Services.Events;
using RassoApi.Services.Events.Interfaces;

namespace RassoApi.Extensions
{
    public static class DIEvent
    {
        /// <summary>
        /// Ajoute les services liés aux événements
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventServices(this IServiceCollection services)
        {
            services.AddSingleton<IEventService, EventService>();


            return services;
        }
    }
}
