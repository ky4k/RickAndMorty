using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RickAndMorty.Models;

namespace RickAndMorty.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class EpisodeController : Controller
    {
        private readonly HttpClient _httpClient;
        private ApplicationContext db;
        public EpisodeController(ApplicationContext ap)
        {
            db = ap;
            _httpClient = new HttpClient();
        }

        [HttpPost("check-episode")]
        public async Task<IActionResult> CheckEpisode(int id)
        {
            string url = $"https://rickandmortyapi.com/api/episode/{id}";
            HttpResponseMessage response = await _httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                string Response = await response.Content.ReadAsStringAsync();//convert in string type
                Episode episode = JsonConvert.DeserializeObject<Episode>(Response);//deserialize in a object
                return Ok(episode);
            }
            else
                return BadRequest("dfs");
        }

        [HttpGet("all-episodes")]
        public async Task<IActionResult> CheckLocation()
        {
            string url = $"https://rickandmortyapi.com/api/episode";
            HttpResponseMessage response = await _httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();

                var episodes = JsonConvert.DeserializeObject<List<Episode>>(resultsArray);
                return Ok(episodes);
            }
            else
                return BadRequest("dfs");
        }
    }
}
