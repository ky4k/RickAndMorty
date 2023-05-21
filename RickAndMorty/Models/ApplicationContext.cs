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
            Database.EnsureCreated(); 
        }
    }
}
