using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Models;

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
        public async Task<IActionResult> CheckPerson(Character request)
        {
            string url = $"https://rickandmortyapi.com/api/character/?name={request.name}";
            //Отправить завпрос
            Character? person = await _httpClient.GetFromJsonAsync<Character>(url);
            if (person != null)
            {
                return person;
            }
            else
                return NotFound();

        }

        //[HttpGet("person")]
        //public async Task<IActionResult> GetPerson(string name)
        //{

        //}
    }
}
