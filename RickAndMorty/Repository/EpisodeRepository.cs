using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RickAndMorty.Models;
using System.Net.Http;
using RickAndMorty.Interfaces;

namespace RickAndMorty.Repository
{
    public class EpisodeRepository: IEpisodeRequester
    {
        string episode_url = "https://rickandmortyapi.com/api/episode";
        HttpClient httpClient = new HttpClient();
        public EpisodeRepository(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<Episode>> GetAllEpisodes()
        {
            HttpResponseMessage response = await httpClient.GetAsync(episode_url);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Episode>>(resultsArray);
                return result;
            }
            else
            {
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }
        }
        public async Task<List<Episode>> GetEpisodesByIDlist(List<int> listID) 
        {
            if (listID == null) throw new ArgumentNullException("list is null");

            var HasNegativeValue = listID.Any(x => x < 0);
            if (HasNegativeValue) throw new ArgumentException("list has negative value");

            List<int> numbers = listID;
            string numbersString = string.Join(",", numbers);
            string url = $"{episode_url}/{numbersString}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                string Response = await response.Content.ReadAsStringAsync();//convert in string type
                var episodes = JsonConvert.DeserializeObject<List<Episode>>(Response);//deserialize in a object
                return episodes;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
        public async Task<Episode> GetEpisodeByID(int id)
        {
            if (id < 0) throw new ArgumentException("id must be more then 0");
            string url = $"{episode_url}/{id}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                string Response = await response.Content.ReadAsStringAsync();//convert in string type
                Episode episode = JsonConvert.DeserializeObject<Episode>(Response);//deserialize in a object
                return episode;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
        public async Task<List<Episode>> GetEpisodeByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            string url = $"{episode_url}/?name={name}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Episode>>(resultsArray);
                return result;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
        public async Task<List<Episode>> GetEpisodeByEpisode(string episode)
        {
            if (string.IsNullOrWhiteSpace(episode))
                throw new ArgumentException("Name cannot be empty.", nameof(episode));
            string url = $"{episode_url}/?episode={episode}";
            HttpResponseMessage response = await httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var result = JsonConvert.DeserializeObject<List<Episode>>(resultsArray);
                return result;
            }
            else
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }
    }
}
