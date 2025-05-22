using Microsoft.EntityFrameworkCore;
using RassoApi.Database;
using RassoApi.Repositories;
using RassoApi.Repositories.Interfaces;
using RassoApi.Services.DB;
using RassoApi.Services.Events;
using RassoApi.Services.Interfaces.DB;
using RassoApi.Services.Interfaces.Events;

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
            _ = services.AddScoped<IDataBaseConnectionService, DataBaseConnectionService>();

            _ = services.AddDbContext<AppDbContext>((sp, options) =>
            {
                var dbService = sp.GetRequiredService<IDataBaseConnectionService>();
                string connectionString = dbService.GetConnectionString();

                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            _ = services.AddSingleton<IMySqlService, MySqlService>();
            _ = services.AddSingleton<IEventRepository, EventRepository>();
            _ = services.AddSingleton<IEventValidator, EventValidator>();



            return services;
        }
    }
}
