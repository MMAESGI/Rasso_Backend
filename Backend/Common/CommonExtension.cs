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
                Console.WriteLine($"Using connection string: {connectionString}");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            return services;
        }

        public static WebApplication UseCommonPackage<TContext>(this WebApplication app) where TContext : DbContext
        {

            // Exemple : seed la base de données
            using (var scope = app.Services.CreateScope())
            {
                TContext db = scope.ServiceProvider.GetRequiredService<TContext>();
                db.Database.Migrate();
            }

            return app;
        }

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            return services;
        }
    }
}
