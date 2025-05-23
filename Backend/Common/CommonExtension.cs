using Common.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;


namespace Common
{
    public static class CommonExtension
    {
        public static IServiceCollection AddCommonServices<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            services.AddScoped<IDataBaseConnectionService, DataBaseConnectionService>();

            services.AddDbContext<TContext>((sp, options) =>
            {
                string connectionString = DataBaseConnectionService.GetConnectionString();

                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            return services;
        }

        public static WebApplication UseCommonPackage(this WebApplication app)
        {

            // Exemple : seed la base de données
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DbContext>();
                db.Database.Migrate();
            }

            return app;
        }
    }
}
