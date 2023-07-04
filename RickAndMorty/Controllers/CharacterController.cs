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
using Azure;
using System.Net.WebSockets;
using Microsoft.EntityFrameworkCore;
using RickAndMorty.Operations;
using RickAndMorty.Interfaces;

namespace RickAndMorty.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class CheckPersonController : ControllerBase
    {
        // Подкллючаем DI. Создаем поля інтерфейсов и приватные поля и используем дальше их.
        HttpClient _httpClient = new HttpClient();//temp
        private IDataOperation db;
        public CheckPersonController(IDataOperation _db)
        {
            db = _db;
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
            Requester<Character> requester = new Requester<Character>();
            var result = await requester.GetResponseAsync(url);
            await db.UpdateData();
            return Ok(result);
        }
    }
}
