using Microsoft.EntityFrameworkCore;

namespace musicShop.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MusicianRole>()
                .HasKey(mr => new { mr.MusicianId, mr.RoleId });
            modelBuilder.Entity<MusicianEnsemble>()
                .HasKey(mr => new { mr.MusicianId, mr.EnsembleId });
        }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<MusicianRole> MusicianRoles { get; set; }
        public DbSet<Composition> Compositions { get; set; }
        public DbSet<Ensemble> Ensembles { get; set; }
        public DbSet<Logging> Loggings { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TypeEnsemble> TypeEnsembles { get; set; }
        public DbSet<TypeLogging> TypeLoggings { get; set; }
}
}
