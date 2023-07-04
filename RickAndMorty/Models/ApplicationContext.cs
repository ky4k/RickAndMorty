using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace RickAndMorty.Models
{
    public class ApplicationContext : DbContext
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
            modelBuilder.Entity<Character>()
                .HasMany(c => c.EpisodesList)
                .WithMany(e => e.CharactersList)
                .UsingEntity(j => j.ToTable("CharacterEpisode"));

            modelBuilder.Entity<Character>()
                .HasOne(c => c.LocationList)
                .WithMany(l => l.Characters)
                .HasForeignKey(c => c.LocationId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Episode>()
                .HasMany(e => e.CharactersList)
                .WithMany(c => c.EpisodesList)
                .UsingEntity(j => j.ToTable("CharacterEpisode"));

            modelBuilder.Entity<Location>()
                .HasMany(l => l.Characters)
                .WithOne(c => c.LocationList)
                .HasForeignKey(c => c.LocationId)
                .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
