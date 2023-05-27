using Microsoft.EntityFrameworkCore;
namespace RickAndMorty.Models
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated(); 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().HasKey(c => new { c.id});
            modelBuilder.Entity<Episode>().HasKey(e => new { e.id });
            modelBuilder.Entity<Location>().HasKey(l => new { l.id });
        }
    }
}
