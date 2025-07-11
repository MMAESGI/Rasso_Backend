using Microsoft.AspNetCore.Identity;
using RassoApi.Mappers;
using RassoApi.Repositories;
using RassoApi.Repositories.Interfaces;
using RassoApi.Services.Events;
using RassoApi.Services.Events.Interfaces;
using RassoApi.Services.Storage;

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
            services.AddScoped<IEventMapper, EventMapper>();
            services.AddScoped<IUserMapper, UserMapper>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ISearchRepository, SearchRepository>();
            services.AddScoped<IEventValidator, EventValidator>();
            services.AddScoped<ISearchService, SearchService>();
            
            // Service de stockage d'images
            services.AddHttpClient<IImageStorageService, ImageStorageService>(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(30);
            });
            services.AddScoped<IImageStorageService, ImageStorageService>();


            return services;
        }
    }
}
