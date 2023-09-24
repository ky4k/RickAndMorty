using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RickAndMorty.Models;
using System.Net.Http;
using RickAndMorty.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace RickAndMorty.Repository
{
    public class EpisodeHttpRepository: IEpisodeRequester
    {
        string episode_url = "https://rickandmortyapi.com/api/episode";
        HttpClient httpClient = new HttpClient();
        IDistributedCache cache;
        public EpisodeHttpRepository(HttpClient httpClient, IDistributedCache cache)
        {
            this.httpClient = httpClient;
            this.cache = cache;
        }
        public async Task<List<Episode>> GetAllEpisodes()
        {
            var cachedData = await cache.GetStringAsync("episode_GetAllEpisodes");
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cacheResult= JsonConvert.DeserializeObject<List<Episode>>(cachedData);
                return cacheResult;
            }
            HttpResponseMessage response = await httpClient.GetAsync(episode_url);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Episode>>(resultsArray);

                var serializedData = JsonConvert.SerializeObject(result);
                await cache.SetStringAsync("episode_GetAllEpisodes", serializedData, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) 
                });
                return result;
            }
            else
            {
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }
        }
        public async Task<List<Episode>> GetEpisodesByIDlist(List<int> listID) 
        {
            if (listID.Count == 0 || listID.Contains(0)) throw new ArgumentNullException("list is null");
            if (listID.Count <= 1) throw new Newtonsoft.Json.JsonSerializationException("List contains less then one value");
            var HasNegativeValue = listID.Any(x => x <= 0);
            if (HasNegativeValue) throw new ArgumentException("list has negative value");

            string cacheKey = "episode_GetEpisodesByIDlist" + string.Join("_", listID.Select(id => id.ToString()));
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Episode>>(cachedData);
                return cachedResult;
            }

            List<int> numbers = listID;
            string numbersString = string.Join(",", numbers);
            string url = $"{episode_url}/{numbersString}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                string Response = await response.Content.ReadAsStringAsync();//convert in string type
                var episodes = JsonConvert.DeserializeObject<List<Episode>>(Response);//deserialize in a object

                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(episodes), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });

                return episodes;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
        public async Task<Episode> GetEpisodeByID(int id)
        {
            if (id < 0) throw new ArgumentException("id must be more then 0");

            var cacheKey = "episode_GetEpisodeByID_" + id.ToString();
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<Episode>(cachedData);
                return cachedResult;
            }

            string url = $"{episode_url}/{id}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response
            if (response.IsSuccessStatusCode)
            {
                string Response = await response.Content.ReadAsStringAsync();//convert in string type
                Episode episode = JsonConvert.DeserializeObject<Episode>(Response);//deserialize in a object

                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(episode), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });

                return episode;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
        public async Task<List<Episode>> GetEpisodeByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            string cacheKey = "episode_GetEpisodeByName_" + name;
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Episode>>(cachedData);
                return cachedResult;
            }

            string url = $"{episode_url}/?name={name}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Episode>>(resultsArray);

                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(result), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });

                return result;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
        public async Task<List<Episode>> GetEpisodeByEpisode(string episode)
        {
            if (string.IsNullOrWhiteSpace(episode))
                throw new ArgumentException("Name cannot be empty.", nameof(episode));

            string cacheKey = "episode_GetEpisodeByEpisode_" + episode;
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Episode>>(cachedData);
                return cachedResult;
            }

            string url = $"{episode_url}/?episode={episode}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Episode>>(resultsArray);

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
