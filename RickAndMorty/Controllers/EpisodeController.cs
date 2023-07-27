using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RickAndMorty.Interfaces;
using RickAndMorty.Models;
using RickAndMorty.Operations;

namespace RickAndMorty.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class EpisodeController : Controller
    {
        private IEpisodeRequester er;
        public EpisodeController(IEpisodeRequester er)
        {
            this.er = er;
        }

        [HttpGet("all-episodes")]
        public async Task<IActionResult> AllEpisodes()
        {
            try
            {

                var result = await er.GetAllEpisodes();
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                //return data from db
                return Content(ex.Message);
            }
        }
        [HttpPost("multiple-episodes")]
        public async Task<IActionResult> MultiplyEpisodes(List<int> list)
        {
            try
            {
                var result = await er.GetEpisodesByIDlist(list);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                //return data from db
                return Content(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return Content(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet("episode-name")]
        public async Task<IActionResult> EpisodeName(string name)
        {
            try
            {
                var result = await er.GetEpisodeByName(name);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                //return data from db
                return Content(ex.Message);
            }
            catch (ArgumentException ex)
            {
                // return text bed news
                return Content(ex.Message);
            }
        }
        [HttpPost("episode-id")]
        public async Task<IActionResult> EpisodeID(int id)
        {
            try
            {
                var result = await er.GetEpisodeByID(id);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                //return data from db looking on status code
                return Content(ex.Message);
            }
            catch (ArgumentException ex)
            {
                // return text 
                return Content(ex.Message);
            }
        }
        [HttpPost("episode")]
        public async Task<IActionResult> Episode(string episode)
        {
            try
            {
                var result = await er.GetEpisodeByEpisode(episode);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                //return data from db looking on status code
                return Content(ex.Message);
            }
            catch (ArgumentException ex)
            {
                // return text 
                return Content(ex.Message);
            }
        }
    }
}
