using Microsoft.AspNetCore.Identity;
using RassoApi.Mappers;
using RassoApi.Repositories;
using RassoApi.Repositories.Interfaces;
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
            services.AddScoped<IEventService, EventService>();
            services.AddSingleton<IEventMapper, EventMapper>();
            services.AddSingleton<IUserMapper, UserMapper>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ISearchRepository, SearchRepository>();
            services.AddScoped<IEventValidator, EventValidator>();
            services.AddScoped<ISearchService, SearchService>();


            return services;
        }
    }
}
