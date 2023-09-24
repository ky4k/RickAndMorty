using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RickAndMorty.Interfaces;
using RickAndMorty.Models;

namespace RickAndMorty.Repository
{
    public class EpisodeDbRepository: IEpisodeDB
    {
        ApplicationContext db;
        IDistributedCache cache;
        public EpisodeDbRepository(ApplicationContext db, IDistributedCache cache)
        {
            this.db = db;
            this.cache = cache;
        }
        public async Task<List<Episode>> GetAllEpisodes()
        {
            var cachedData = await cache.GetStringAsync("episode_GetAllEpisodes");
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cacheResult = JsonConvert.DeserializeObject<List<Episode>>(cachedData);
                return cacheResult;
            }

            List<Episode> episodes = await db.Episodes.ToListAsync();
            if (episodes.Any())
            {
                await cache.SetStringAsync("episode_GetAllEpisodes", JsonConvert.SerializeObject(episodes), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) 
                });
                return episodes;
            }
            else
            {
                throw new ArgumentNullException("list is null");
            }
        }
        public async Task<List<Episode>> GetEpisodesByIDlist(List<int> listID)
        {
            if (listID == null) throw new ArgumentNullException("list is null");

            var HasNegativeValue = listID.Any(x => x < 0);
            if (HasNegativeValue) throw new ArgumentException("list has negative value");

            string cacheKey = "character_GetEpisodesByIDlist" + string.Join("_", listID.Select(id => id.ToString()));
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Episode>>(cachedData);
                return cachedResult;
            }

            List<Episode> episodes = await db.Episodes.Where(c => listID.Contains(c.id)).ToListAsync();

            if (episodes.Any())
            {
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(episodes), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                return episodes;
            }
            else
            {
                throw new ArgumentNullException("list is null");
            }
        }
        public async Task<Episode> GetEpisodeByID(int id)
        {
            if (id < 0) throw new ArgumentException("id must be more then 0");

            var cacheKey = "character_GetEpisodeByID_" + id.ToString();
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<Episode>(cachedData);
                return cachedResult;
            }

            var episode = await db.Episodes.Where(c => c.id == id).FirstOrDefaultAsync();
            if (!(episode is null))
            {
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(episode), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                return episode;
            }
            else
            {
                throw new ArgumentNullException("list is null");
            }
        }
        public async Task<List<Episode>> GetEpisodeByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            string cacheKey = "character_GetEpisodeByName_" + name;
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Episode>>(cachedData);
                return cachedResult;
            }

            List<Episode> episodes = await db.Episodes.Where(c => name.Contains(c.name)).ToListAsync();
            if (episodes.Any())
            {
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(episodes), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                return episodes;
            }
            else
            {
                throw new ArgumentNullException("list is null");
            }
        }
        public async Task<List<Episode>> GetEpisodeByEpisode(string episode)
        {
            if (string.IsNullOrWhiteSpace(episode))
                throw new ArgumentException("Name cannot be empty.", nameof(episode));

            string cacheKey = "character_GetEpisodeByEpisode_" + episode;
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Episode>>(cachedData);
                return cachedResult;
            }

            List<Episode> episodes = await db.Episodes.Where(c => episode.Contains(c.episode)).ToListAsync();
            if (episodes.Any())
            {
                return episodes;
            }
            else
            {
                throw new ArgumentNullException("list is null");
            }
        }
    }
}
