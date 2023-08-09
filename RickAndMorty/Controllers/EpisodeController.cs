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
        private IEpisodeDB edb;
        private ILogger _logger;
        private ILogger _argumentLogger;
        public EpisodeController(IEpisodeRequester er, IEpisodeDB edb, ILogger<EpisodeController> logger
            , ILoggerFactory loggerFactory)
        {
            this.er = er;
            this.edb = edb;
            _logger = logger;
            _argumentLogger = loggerFactory.CreateLogger("ArgumentLogger");
        }

        [HttpGet("all-episodes")]
        public async Task<IActionResult> AllEpisodes()
        {
            try
            {
                var result = await er.GetAllEpisodes();
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                var res = await edb.GetAllEpisodes();
                _logger.LogError(ex.Message, "Get data from data base");
                return Ok(res);
            }
        }
        [HttpPost("multiple-episodes")]
        public async Task<IActionResult> MultiplyEpisodes(List<int> list)
        {
            try
            {
                var result = await er.GetEpisodesByIDlist(list);
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                var res = await edb.GetEpisodesByIDlist(list);
                _logger.LogError(ex.Message, "Get data from data base");
                return Ok(res);
            }
            catch (ArgumentNullException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content("List is empty or contains null");
            }
            catch (ArgumentException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content(ex.Message);
            }
            catch (Newtonsoft.Json.JsonSerializationException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content("List contains less then one value");
            }
        }
        [HttpGet("episode-name")]
        public async Task<IActionResult> EpisodeName(string name)
        {
            try
            {
                var result = await er.GetEpisodeByName(name);
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                var res = await edb.GetEpisodeByName(name);
                _logger.LogError(ex.Message, "Get data from data base");
                return Ok(res);
            }
            catch (ArgumentException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content("Name cannot be empty");
            }
        }
        [HttpPost("episode-id")]
        public async Task<IActionResult> EpisodeID(int id)
        {
            try
            {
                var result = await er.GetEpisodeByID(id);
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                var res = await edb.GetEpisodeByID(id);
                _logger.LogError(ex.Message, "Get data from data base");
                return Ok(res);
            }
            catch (ArgumentException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content("Name cannot be empty");
            }
        }
        [HttpPost("episode")]
        public async Task<IActionResult> Episode(string episode)
        {
            try
            {
                var result = await er.GetEpisodeByEpisode(episode);
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                var res = await edb.GetEpisodeByEpisode(episode);
                _logger.LogError(ex.Message, "Get data from data base");
                return Ok(res);
            }
            catch (ArgumentException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content("Name cannot be empty");
            }
        }
    }
}
