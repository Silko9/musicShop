using Microsoft.EntityFrameworkCore;
using musicShop.Models;

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
        }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Composition> Compositions { get; set; }
        public DbSet<Ensemble> Ensembles { get; set; }
        public DbSet<Logging> Loggings { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TypeEnsemble> TypeEnsembles { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<TypeLogging> TypeLogging { get; set; }
    }
}
