using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Models;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using System;
using System.Security.Cryptography.Xml;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RickAndMorty.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class CheckPersonController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        //public CheckPersonController(ApplicationContext ap)
        //{
        //    application = ap;
        //    _httpClient = new HttpClient();
        //}
        public CheckPersonController()
        {
            _httpClient = new HttpClient();
        }

        [HttpPost("check-person")]
        public async Task<IActionResult> CheckPerson(int id)
        {
            string url = $"https://rickandmortyapi.com/api/episode/{id}";
            HttpResponseMessage response = await _httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                string Response = await response.Content.ReadAsStringAsync();//convert in string type
                Episode episode = JsonConvert.DeserializeObject<Episode>(Response);//serialize in a object
                return Ok(episode);
            }
            else
                return BadRequest("dfs");
        }
    }
}
