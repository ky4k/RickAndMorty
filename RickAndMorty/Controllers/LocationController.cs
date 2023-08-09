using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RickAndMorty.Interfaces;
using RickAndMorty.Models;
using RickAndMorty.Operations;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace RickAndMorty.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class LocationController : Controller
    {
        private ILocationRequester lr;
        private ILocationDB ldb;
        private ILogger _logger;
        private ILogger _argumentLogger;
        public LocationController(ILocationRequester lr, ILocationDB ldb, ILogger<LocationController> log
            , ILoggerFactory loggerFactory)
        {
            this.lr = lr;
            this.ldb = ldb;
            this._logger = log;
            _argumentLogger = loggerFactory.CreateLogger("ArgumentLogger");
        }

        [HttpGet("all-locations")]
        public async Task<IActionResult> AllLocations()
        {
            try
            {
                var result = await lr.GetAll();
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch(HttpRequestException ex)
            {
                _logger.LogError(ex.Message, "Get data from data base");
                var res = await ldb.GetAll();
                return Ok(res);
            }
        }
        [HttpPost("multiple-locations")]
        public async Task<IActionResult> MultiplyLocations(List<int> list)
        {
            try
            {
                var result = await lr.GetByIDlist(list);
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch(HttpRequestException ex)
            {
                var res = await ldb.GetByIDlist(list);
                _logger.LogError(ex.Message, "Get data from data base");
                return Ok(res);
            }
            catch (ArgumentNullException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content("Episodes' list is empty");
            }
            catch (ArgumentException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content("Name cannot be empty");
            }
            catch (Newtonsoft.Json.JsonSerializationException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content("List contains less then one value");
            }
        }
        [HttpGet("location-id")]
        public async Task<IActionResult> LocationID(int id)
        {
            try
            {
                var result = await lr.GetByID(id);
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch (HttpRequestException ex) 
            {
                var res = await ldb.GetByID(id);
                _logger.LogError(ex.Message, "Get data from data base");
                return Ok(res);
            }
            catch (ArgumentException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content("Name cannot be empty");
            }
        }
        [HttpGet("location-type")]
        public async Task<IActionResult> LocationType(string type)
        {
            try
            {
                var result = await lr.GetByType(type);
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                var res = await ldb.GetByType(type);
                _logger.LogError(ex.Message, "Get data from data base");
                return Ok(res);
            }
            catch (ArgumentException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content("Name cannot be empty");
            }
        }
        [HttpGet("location-demension")]
        public async Task<IActionResult> LocationDemesion(string demension)
        {
            try
            {
                var result = await lr.GetByDimension(demension);
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                var res = await ldb.GetByDimension(demension);
                _logger.LogError(ex.Message, "Get data from data base");
                return Ok(res);
            }
            catch (ArgumentException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content("Name cannot be empty");
            }
        }
        [HttpGet("location-name")]
        public async Task<IActionResult> LocationName(string name)
        {
            try
            {
                var result = await lr.GetByName(name);
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                var res = await ldb.GetByName(name);
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
