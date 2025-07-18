﻿using Common.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System;


namespace Common
{
    public static class CommonExtension
    {
        public static IServiceCollection AddCommonServices<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            services.AddScoped<IDataBaseConnectionService, DataBaseConnectionService>();

            var isSwaggerCli = AppDomain.CurrentDomain.FriendlyName.Contains("swagger", StringComparison.OrdinalIgnoreCase);

            if (!isSwaggerCli)
            {
                services.AddDbContext<TContext>((sp, options) =>
                {
                    string connectionString = DataBaseConnectionService.GetConnectionString();
                    Console.WriteLine($"Using connection string: {connectionString}");
                    options.UseMySQL(connectionString);
                });
            }
            else
            {
                // Ajout d'un DbContext InMemory pour Swagger
                services.AddDbContext<TContext>(options =>
                    options.UseInMemoryDatabase("SwaggerInMemoryDb"));
            }
            return services;
        }

        public static WebApplication UseCommonPackage<TContext>(this WebApplication app) where TContext : DbContext
        {

            var isSwaggerCli = AppDomain.CurrentDomain.FriendlyName.Contains("swagger", StringComparison.OrdinalIgnoreCase);

            if (!isSwaggerCli)
            {
                // Exemple : seed la base de données
                using (var scope = app.Services.CreateScope())
                {
                    TContext db = scope.ServiceProvider.GetRequiredService<TContext>();
                    db.Database.Migrate();
                }
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
