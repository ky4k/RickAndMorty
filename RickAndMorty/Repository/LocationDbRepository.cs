using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RickAndMorty.Interfaces;
using RickAndMorty.Models;

namespace RickAndMorty.Repository
{
    public class LocationDbRepository: ILocationDB
    {
        ApplicationContext db;
        IDistributedCache cache;
        public LocationDbRepository(ApplicationContext db, IDistributedCache cache) 
        { 
            this.db = db; 
            this.cache = cache;
        }
        public async Task<List<Location>> GetAll()
        {
            var cachedData = await cache.GetStringAsync("location_GetAll");
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cacheResult = JsonConvert.DeserializeObject<List<Location>>(cachedData);
                return cacheResult;
            }

            List<Location> locations = await db.Locations.ToListAsync();
            if (locations.Any())
            {
                var serializedData = JsonConvert.SerializeObject(locations);
                await cache.SetStringAsync("location_GetAll", serializedData, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                return locations;
            }
            else
            {
                throw new ArgumentNullException("list is null");
            }
        }
        public async Task<List<Location>> GetByIDlist(List<int> listID)
        {
            if (listID == null) throw new ArgumentNullException("list is null");

            var HasNegativeValue = listID.Any(x => x < 0);
            if (HasNegativeValue) throw new ArgumentException("list has negative value");

            string cacheKey = "location_GetByIDlist" + string.Join("_", listID.Select(id => id.ToString()));
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Location>>(cachedData);
                return cachedResult;
            }

            List<Location> locations = await db.Locations.Where(c => listID.Contains(c.id)).ToListAsync();

            if (locations.Any())
            {
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(locations), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                return locations;
            }
            else
            {
                throw new ArgumentNullException("list is null");
            }
        }
        public async Task<Location> GetByID(int id)
        {
            if (id < 0) throw new ArgumentException("id must be more then 0");

            var cacheKey = "location_GetByID_" + id.ToString();
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<Location>(cachedData);
                return cachedResult;
            }

            var location = await db.Locations.Where(c => c.id == id).FirstOrDefaultAsync();
            if (!(location is null))
            {
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(location), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                return location;
            }
            else
            {
                throw new ArgumentNullException("list is null");
            }
        }
        public async Task<List<Location>> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            string cacheKey = "location_GetByName_" + name;
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Location>>(cachedData);
                return cachedResult;
            }

            List<Location> locations = await db.Locations.Where(c => name.Contains(c.name)).ToListAsync();
            if (locations.Any())
            {
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(locations), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                return locations;
            }
            else
            {
                throw new ArgumentNullException("list is null");
            }
        }
        public async Task<List<Location>> GetByType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Name cannot be empty.", nameof(type));

            string cacheKey = "location_GetByType_" + type;
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Location>>(cachedData);
                return cachedResult;
            }

            List<Location> locations = await db.Locations.Where(c => c.type == type).ToListAsync();
            if (locations.Any())
            {
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(locations), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                return locations;
            }
            else
            {
                throw new ArgumentNullException("list is null");
            }
        }
        public async Task<List<Location>> GetByDimension(string dimension)
        {
            if (string.IsNullOrWhiteSpace(dimension))
                throw new ArgumentException("Name cannot be empty.", nameof(dimension));

            string cacheKey = "location_GetByDimension_" + dimension;
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Location>>(cachedData);
                return cachedResult;
            }

            List<Location> locations = await db.Locations.Where(c => c.dimension == dimension).ToListAsync();
            if (locations.Any())
            {
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(locations), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                return locations;
            }
            else
            {
                throw new ArgumentNullException("list is null");
            }
        }
    }
}
