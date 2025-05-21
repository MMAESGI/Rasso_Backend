using Microsoft.EntityFrameworkCore;
using RassoApi.Models.EventModels;
using RassoApi.Models;

namespace RassoApi.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Event> Events => Set<Event>();
        public DbSet<EventMedia> EventImages => Set<EventMedia>();
        public DbSet<EventStatus> EventStatuses => Set<EventStatus>();
        public DbSet<RefusalReason> RefusalReasons => Set<RefusalReason>();
        public DbSet<EventParticipant> EventParticipants => Set<EventParticipant>();
        public DbSet<Favorite> Favorites => Set<Favorite>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
