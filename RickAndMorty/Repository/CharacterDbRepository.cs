using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RickAndMorty.Interfaces;
using RickAndMorty.Models;
using System.Collections.Generic;
using System.ComponentModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RickAndMorty.Repository
{
    public class CharacterDbRepository : ICharacterDB
    {
        ApplicationContext db;
        IDistributedCache cache;
        public CharacterDbRepository(ApplicationContext db, IDistributedCache cache)
        {
            this.db = db;
            this.cache = cache;
        }
        public async Task<List<Character>> GetAll()
        {
            var cachedData = await cache.GetStringAsync("character_GetAll");
            if (cachedData != null)
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Character>>(cachedData);
                return cachedResult;
            }

            var characters = await db.Characters.ToListAsync();
            if (characters.Any())
            {
                await cache.SetStringAsync("character_GetAll", JsonConvert.SerializeObject(characters), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) 
                });

                return characters;
            }
            else
            {
                throw new ArgumentNullException("list is null");
            }

        }
        public async Task<List<Character>> GetByIDlist(List<int> listID)
        {
            if (listID == null) throw new ArgumentNullException("list is null");

            var HasNegativeValue = listID.Any(x => x < 0);
            if (HasNegativeValue) throw new ArgumentException("list has negative value");
            string cacheKey = "character_GetByIDlist" + string.Join("_", listID.Select(id => id.ToString()));

            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Character>>(cachedData);
                return cachedResult;
            }

            List<Character> characters = await db.Characters.Where(c => listID.Contains(c.id)).ToListAsync();

            if (characters.Any())
            {
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(characters), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                return characters;
            }
            else
            {
                throw new ArgumentNullException("list is null");
            }
        }
        public async Task<Character> GetID(int id)
        {
            if (id <= 0) throw new ArgumentException("id must be more then 0");
            var cacheKey = "character_GetID_" + id.ToString();
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<Character>(cachedData);
                return cachedResult;
            }
            var character = await db.Characters.Where(c=>c.id== id).FirstOrDefaultAsync();
            if (!(character is null))
            {
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(character), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) 
                });
                return character;
            }
            else
            {
                throw new ArgumentNullException("list is null");
            }
        }
        public async Task<List<Character>> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            string cacheKey = "character_GetByName_" + name;
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Character>>(cachedData);
                return cachedResult;
            }
            List<Character> characters = await db.Characters.Where(c=>name.Contains(c.name)).ToListAsync();
            if (characters.Any())
            {
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(characters), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                return characters;
            }
            else
            {
                throw new ArgumentNullException("list is null");
            }
        }
        public async Task<List<Character>> GetCharacterStatus(string name, string status)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Gender cannot be empty.", nameof(status));

            string cacheKey = $"character_GetCharacterStatus_{name}_{status}";
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Character>>(cachedData);
                return cachedResult;
            }

            var characters = db.Characters.Where(c =>c.name==name);
            if (characters.Any())
            {
                characters = characters.Where(c => c.status == status);
                List<Character> result = await characters.ToListAsync();
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(result), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                return result;
            }
            else
            {
                throw new ArgumentNullException("list is null");
            }
        }
        public async Task<List<Character>> GetCharacterBySpecies(string species)
        {
            if (string.IsNullOrWhiteSpace(species))
                throw new ArgumentException("Name cannot be empty.", nameof(species));

            var cacheKey = $"character_GetCharacterBySpecies_{species}";
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Character>>(cachedData);
                return cachedResult;
            }

            List<Character> characters = await db.Characters.Where(c => c.species==species).ToListAsync();
            if (characters.Any())
            {
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(characters), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                return characters;
            }
            else
            {
                throw new ArgumentNullException("list is null");
            }
        }
        public async Task<List<Character>> GetCharacterByType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Name cannot be empty.", nameof(type));

            var cacheKey = $"character_GetCharacterByType_{type}";
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Character>>(cachedData);
                return cachedResult;
            }

            List<Character> characters = await db.Characters.Where(c => c.type == type).ToListAsync();
            if (characters.Any())
            {
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(characters), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                return characters;
            }
            else
            {
                throw new ArgumentNullException("list is null");
            }
        }
        public async Task<List<Character>> GetCharacteGender(string name, string gender)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            if (string.IsNullOrWhiteSpace(gender))
                throw new ArgumentException("Gender cannot be empty.", nameof(gender));

            var cacheKey = $"character_GetCharacteGender_{name}_{gender}";
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Character>>(cachedData);
                return cachedResult;
            }

            var characters = db.Characters.Where(c => c.name == name);
            if (characters.Any())
            {
                characters = characters.Where(c => c.gender == gender);
                List<Character> result = await characters.ToListAsync();
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(result), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                return result;
            }
            else
                throw new ArgumentNullException("list is null");
        }
    }
}
