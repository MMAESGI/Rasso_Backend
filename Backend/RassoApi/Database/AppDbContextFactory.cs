using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using RassoApi.Services.Interfaces.DB;

namespace RassoApi.Database
{
    /// <summary>
    /// Factory utilisée lors de l'exécution de la commmande utilisé pour effectuer une migration EF
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IDataBaseConnectionService dbService = new DataBaseConnectionService(); // PAS injecté, mais utilisé manuellement
            string connectionString = dbService.GetConnectionString();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
    
}
