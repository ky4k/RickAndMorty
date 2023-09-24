using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RickAndMorty.Interfaces;
using RickAndMorty.Models;
using System.Net.Http;
using System;
using System.Reflection;
using Microsoft.Extensions.Caching.Distributed;

namespace RickAndMorty.Repository
{
    public class CharacteHttpRepository : ICharacterRequester
    {
        string character_url = "https://rickandmortyapi.com/api/character";
        HttpClient httpClient = new HttpClient();
        IDistributedCache cache;
        public CharacteHttpRepository(HttpClient httpClient, IDistributedCache cache)
        {
            this.httpClient = httpClient;
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

            HttpResponseMessage response = await httpClient.GetAsync(character_url);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Character>>(resultsArray);

                var serializedData = JsonConvert.SerializeObject(result);
                await cache.SetStringAsync("character_GetAll", serializedData, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) // Настройте время истечения кэша
                });

                return result;
            }
            else
            {
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }
        }
        public async Task<List<Character>> GetByIDlist(List<int> listID)
        {
            if (listID.Count == 0 || listID.Contains(0)) throw new ArgumentNullException("list is null");
            if (listID.Count <= 1) throw new Newtonsoft.Json.JsonSerializationException("List contains less then one value");
            var HasNegativeValue = listID.Any(x => x <=0);
            if (HasNegativeValue) throw new Newtonsoft.Json.JsonSerializationException("list has negative value");

            string cacheKey = "character_GetByIDlist" + string.Join("_", listID.Select(id => id.ToString()));

            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Character>>(cachedData);
                return cachedResult;
            }

            List<int> numbers = listID;
            string numbersString = string.Join(",", numbers);
            string url = $"{character_url}/{numbersString}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response
            if (response.IsSuccessStatusCode)
            {
                string Response = await response.Content.ReadAsStringAsync();//convert in string type
                var characters = JsonConvert.DeserializeObject<List<Character>>(Response);//deserialize in a object
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(characters), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) 
                });

                return characters;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
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
            string url = $"{character_url}/{id}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                string Response = await response.Content.ReadAsStringAsync();//convert in string type
                Character character = JsonConvert.DeserializeObject<Character>(Response);//deserialize in a object
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(character), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) 
                });
                return character;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
        public async Task<List<Character>> GetByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new System.ArgumentException("Name cannot be empty.", nameof(name));
            if (string.IsNullOrEmpty(name)) throw new System.ArgumentNullException("string is null");

            string cacheKey = "character_GetByName_" + name;
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Character>>(cachedData);
                return cachedResult;
            }
            string url = $"{character_url}/?name={name}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Character>>(resultsArray);

                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(result), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) 
                });

                return result;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
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

            string url = $"{character_url}/?name={name}&status={status}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Character>>(resultsArray);

                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(result), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) 
                });

                return result;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
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

            string url = $"{character_url}/?species={species}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Character>>(resultsArray);
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(result), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                return result;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
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

            string url = $"{character_url}/?type={type}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Character>>(resultsArray);

                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(result), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                return result;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
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
                var cachedResult= JsonConvert.DeserializeObject<List<Character>>(cachedData);
                return cachedResult;
            }
                string url = $"{character_url}/?name={name}&gender={gender}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Character>>(resultsArray);
                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(result), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                return result;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
    }
}
