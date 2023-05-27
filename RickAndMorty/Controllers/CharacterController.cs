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
                //Next step add this data in DB
                //db.Characters.Add(character);
                //await db.SaveChangesAsync();
                return Ok(character);
            }
            else
                return BadRequest("dfs");
        }

        [HttpGet("all-characters")]
        public async Task<IActionResult> CheckLocation()
        {
            string url = $"https://rickandmortyapi.com/api/character";
            HttpResponseMessage response = await _httpClient.GetAsync(url);//GET request and get response

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);
                var resultsArray = jsonObject["results"].ToString();

                var characters = JsonConvert.DeserializeObject<List<Character>>(resultsArray);
                return Ok(characters);
            }
            else
                return BadRequest("dfs");
        }
    }
}
//Добавлеие даных в базу данных
//1) Вариант: добавить в эту модель внешний ключ, который будет сохранять ID, а не ссылки. Потому что ссылки 
// по сути указывают на ID episode/character/location. Причем, связь скорей всего оставляем много-ко-многим
//Реализация: по сути надо заменить string[] на типизированый лист без добовлений доп. свойств, если ляжет,
//то убираем типизацию -------- не рабочий
//1.1) попробовать явно указать несколько внешних ключей и и сделать привязку по ним
//ИЛИ
//Оставить туде модель, но добавить типизированый лист
//2) Вариант: добавить типизированные функции как приватные (но чтоб они отображались в БД), а так же добавить 
//функцию, которая будет послдние цифры определять как ID и добавалять в листы.