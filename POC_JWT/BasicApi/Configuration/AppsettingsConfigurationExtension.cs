namespace BasicApi.Configuration
{
    public static class AppsettingsConfigurationExtension
    {
        public static IServiceCollection AddAppSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<AppsettingsConfiguration>(configuration.GetSection("Configuration"));


            return services;
        }
    }
}
