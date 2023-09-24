using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RickAndMorty.Interfaces;
using RickAndMorty.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace RickAndMorty.Repository
{
    public class LocationHttpRepository: ILocationRequester
    {
        string location_url = "https://rickandmortyapi.com/api/location";
        HttpClient httpClient = new HttpClient();
        IDistributedCache cache;
        public LocationHttpRepository(HttpClient httpClient, IDistributedCache cache)
        {
            this.httpClient = httpClient;
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

            HttpResponseMessage response = await httpClient.GetAsync(location_url);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Location>>(resultsArray);

                var serializedData = JsonConvert.SerializeObject(result);
                await cache.SetStringAsync("location_GetAll", serializedData, new DistributedCacheEntryOptions
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
        public async Task<List<Location>> GetByIDlist(List<int> listID)
        {
            if (listID.Count == 0 || listID.Contains(0)) throw new ArgumentNullException("list is null");
            if (listID.Count <= 1) throw new Newtonsoft.Json.JsonSerializationException("List contains less then one value");
            var HasNegativeValue = listID.Any(x => x <= 0);
            if (HasNegativeValue) throw new ArgumentException("list has negative value");

            string cacheKey = "location_GetByIDlist" + string.Join("_", listID.Select(id => id.ToString()));
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonConvert.DeserializeObject<List<Location>>(cachedData);
                return cachedResult;
            }

            List<int> numbers = listID;
            string numbersString = string.Join(",", numbers);
            string url = $"{location_url}/{numbersString}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                string Response = await response.Content.ReadAsStringAsync();//convert in string type
                var locations = JsonConvert.DeserializeObject<List<Location>>(Response);//deserialize in a object

                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(locations), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });

                return locations;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
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

            string url = $"{location_url}/{id}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                string Response = await response.Content.ReadAsStringAsync();//convert in string type
                Location location = JsonConvert.DeserializeObject<Location>(Response);//deserialize in a object

                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(location), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });

                return location;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
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

            string url = $"{location_url}/?name={name}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Location>>(resultsArray);

                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(result), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });

                return result;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
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

            string url = $"{location_url}/?type={type}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Location>>(resultsArray);

                await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(result), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });

                return result;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
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

            string url = $"{location_url}/?dimension={dimension}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Location>>(resultsArray);

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
