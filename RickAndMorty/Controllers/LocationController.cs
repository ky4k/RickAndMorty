using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RickAndMorty.Data;
using RickAndMorty.Models;
using RickAndMorty.Operations;
using System.Net.Http;

namespace RickAndMorty.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class LocationController : Controller
    {
        private readonly HttpClient _httpClient;
        private ApplicationContext db;
        public LocationController(ApplicationContext ap)
        {
            db = ap;
            _httpClient = new HttpClient();
        }
        [HttpPost("check-location")]
        public async Task<IActionResult> CheckLocation(int id)
        {
            string url = $"https://rickandmortyapi.com/api/location/{id}";
            HttpResponseMessage response = await _httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                string Response = await response.Content.ReadAsStringAsync();//convert in string type
                Location location = JsonConvert.DeserializeObject<Location>(Response);//deserialize in a object
                return Ok(location);
            }
            else
                return BadRequest("dfs");


        }

        [HttpGet("all-locations")]
        public async Task<IActionResult> CheckLocation()
        {
            string url = $"https://rickandmortyapi.com/api/location";
            //HttpResponseMessage response = await _httpClient.GetAsync(url);//GET request and get response

            //if (response.IsSuccessStatusCode)
            //{
            //    var responseContent = await response.Content.ReadAsStringAsync();
            //    var jsonObject = JObject.Parse(responseContent);
            //    var resultsArray = jsonObject["results"].ToString();
            //    var locations = JsonConvert.DeserializeObject<List<Location>>(resultsArray);

            //    db.Locations.AddRange(locations);

            //    await db.SaveChangesAsync();
            //    return Ok(locations);
            //}
            //else
            //    return BadRequest("dfs");
            Requester<Location> requester= new Requester<Location>();
            var result = await requester.GetResponseAsync(url);
            return Ok(result);
        }
    }
}
