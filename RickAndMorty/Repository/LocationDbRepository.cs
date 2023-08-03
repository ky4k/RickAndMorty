using Microsoft.EntityFrameworkCore;
using RickAndMorty.Interfaces;
using RickAndMorty.Models;

namespace RickAndMorty.Repository
{
    public class LocationDbRepository: ILocationDB
    {
        ApplicationContext db;
        public LocationDbRepository(ApplicationContext db) { this.db = db; }
        public async Task<List<Location>> GetAll()
        {
            List<Location> locations = await db.Locations.ToListAsync();
            if (locations.Any())
                return locations;
            else
                throw new ArgumentNullException("list is null");
        }
        public async Task<List<Location>> GetByIDlist(List<int> listID)
        {
            if (listID == null) throw new ArgumentNullException("list is null");

            var HasNegativeValue = listID.Any(x => x < 0);
            if (HasNegativeValue) throw new ArgumentException("list has negative value");

            List<Location> locations = await db.Locations.Where(c => listID.Contains(c.id)).ToListAsync();

            if (locations.Any())
                return locations;
            else
                throw new ArgumentNullException("list is null");
        }
        public async Task<Location> GetByID(int id)
        {
            if (id < 0) throw new ArgumentException("id must be more then 0");
            var locations = await db.Locations.Where(c => c.id == id).FirstOrDefaultAsync();
            if (!(locations is null))
                return locations;
            else
                throw new ArgumentNullException("list is null");
        }
        public async Task<List<Location>> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            List<Location> locations = await db.Locations.Where(c => name.Contains(c.name)).ToListAsync();
            if (locations.Any())
                return locations;
            else
                throw new ArgumentNullException("list is null");
        }
        public async Task<List<Location>> GetByType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Name cannot be empty.", nameof(type));
            List<Location> locations = await db.Locations.Where(c => c.type == type).ToListAsync();
            if (locations.Any())
                return locations;
            else
                throw new ArgumentNullException("list is null");
        }
        public async Task<List<Location>> GetByDimension(string dimension)
        {
            if (string.IsNullOrWhiteSpace(dimension))
                throw new ArgumentException("Name cannot be empty.", nameof(dimension));
            List<Location> locations = await db.Locations.Where(c => c.dimension == dimension).ToListAsync();
            if (locations.Any())
                return locations;
            else
                throw new ArgumentNullException("list is null");
        }
    }
}
