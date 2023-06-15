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
using RickAndMorty.Data;
using Azure;
using System.Net.WebSockets;
using Microsoft.EntityFrameworkCore;
using RickAndMorty.Operations;

namespace RickAndMorty.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class CheckPersonController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private ApplicationContext db;
        public CheckPersonController(ApplicationContext ap)
        {
            db = ap;
            _httpClient = new HttpClient();
        }

        [HttpPost("check-person")]
        public async Task<IActionResult> CheckPerson(int id)
        {
            string url = $"https://rickandmortyapi.com/api/character/{id}";
            HttpResponseMessage response = await _httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                string Response = await response.Content.ReadAsStringAsync();//convert in string type
                Character character = JsonConvert.DeserializeObject<Character>(Response);//deserialize in a object
                return Ok(character);
            }
            else
                return BadRequest("dfs");
        }

        [HttpGet("all-characters")]
        public async Task<IActionResult> CheckLocation()
        {
            string url = $"https://rickandmortyapi.com/api/character";
            //HttpResponseMessage response = await _httpClient.GetAsync(url);//GET request and get response

            //if (response.IsSuccessStatusCode)
            //{
            //    var responseContent = await response.Content.ReadAsStringAsync();
            //    var jsonObject = JObject.Parse(responseContent);
            //    var resultsArray = jsonObject["results"].ToString();
            //    var characters = JsonConvert.DeserializeObject<List<Character>>(resultsArray);
            //    дале перебрать массив ссылок, те ссылки которые совпвдвют - добавляем в соответстувующее связи
            //    await db.Characters.AddRangeAsync(characters);
            //    await db.SaveChangesAsync();

            //    return Ok(characters);
            //}
            //else
            //    return BadRequest("dfs");
            Requester<Character> requester = new Requester<Character>();
            var result = await requester.GetResponseAsync(url);
            return Ok(result);
        }
    }
}
//Нужно добавить такие функции:
//1)Выдача 
