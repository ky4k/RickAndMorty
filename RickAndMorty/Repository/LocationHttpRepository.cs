using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RickAndMorty.Interfaces;
using RickAndMorty.Models;

namespace RickAndMorty.Repository
{
    public class LocationHttpRepository: ILocationRequester
    {
        string location_url = "https://rickandmortyapi.com/api/location";
        HttpClient httpClient = new HttpClient();
        public LocationHttpRepository(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<Location>> GetAll()
        {
            HttpResponseMessage response = await httpClient.GetAsync(location_url);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Location>>(resultsArray);
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

            List<int> numbers = listID;
            string numbersString = string.Join(",", numbers);
            string url = $"{location_url}/{numbersString}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                string Response = await response.Content.ReadAsStringAsync();//convert in string type
                var locations = JsonConvert.DeserializeObject<List<Location>>(Response);//deserialize in a object
                return locations;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
        public async Task<Location> GetByID(int id)
        {
            if (id < 0) throw new ArgumentException("id must be more then 0");

            string url = $"{location_url}/{id}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                string Response = await response.Content.ReadAsStringAsync();//convert in string type
                Location location = JsonConvert.DeserializeObject<Location>(Response);//deserialize in a object
                return location;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
        public async Task<List<Location>> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            string url = $"{location_url}/?name={name}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Location>>(resultsArray);
                return result;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
        public async Task<List<Location>> GetByType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Name cannot be empty.", nameof(type));

            string url = $"{location_url}/?type={type}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Location>>(resultsArray);
                return result;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
        public async Task<List<Location>> GetByDimension(string dimension)
        {
            if (string.IsNullOrWhiteSpace(dimension))
                throw new ArgumentException("Name cannot be empty.", nameof(dimension));

            string url = $"{location_url}/?dimension={dimension}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Location>>(resultsArray);
                return result;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
    }
}
