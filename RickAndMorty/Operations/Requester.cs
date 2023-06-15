using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RickAndMorty.Interfaces;
using RickAndMorty.Models;
using System.Net.Http;

namespace RickAndMorty.Operations
{
    public class Requester<T>:IRequester<T>
    {
        public async Task<List<T>> GetResponseAsync(string url)
        {
            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();
                var locations = JsonConvert.DeserializeObject<List<T>>(resultsArray);
                return locations;
            }
            else
            {
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }
        } 
    }
}
