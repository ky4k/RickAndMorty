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
using RickAndMorty.Repository;
using System.Collections.Generic;
using System.ComponentModel;

namespace RickAndMorty.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class CharacteerController : ControllerBase
    {
        
        private ICharacterRequester cr;
        private ICharacterDB cdb;
        private ILogger _logger;
        private ILogger _argumentLogger;
        public CharacteerController(ICharacterRequester cr, ICharacterDB cdb, ILogger<CharacteerController> logger
          , ILoggerFactory loggerFactory)
        {
            this.cr = cr;
            this.cdb = cdb;
            _logger = logger;
            _argumentLogger = loggerFactory.CreateLogger("ArgumentLogger");
        }

        [HttpPost("multiple-characters")]
        public async Task<IActionResult> ListSomeCharacters(List<int> list)//ИСКЛЮЧЕНИЕ: НУЖНО ОБЯЗАТКЛЬНО ВСТАВЛЯТЬ БОЛЬШЕ 1 ЗНАЧЕНИЯ
        {
            try
            {
                var result = await cr.GetByIDlist(list);
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                var res = await cdb.GetByIDlist(list);
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
            catch(Newtonsoft.Json.JsonSerializationException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content("List contains less then one value");
            }
        }

        [HttpGet("all-characters")]
        public async Task<IActionResult> AllCharacters()
        {
            try
            {
                var result = await cr.GetAll();
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                var res = await cdb.GetAll();
                _logger.LogError(ex.Message, "Get data from data base");
                return Ok(res);
            }
        }

        [HttpGet("character-id")]
        public async Task<IActionResult> CharacteID(int id)//ОБРАБОТАТЬ ПУСТІЕ ЗНАЧЕНИЯ
        {
            try
            {
                var result = await cr.GetID(id);
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                var res = await cdb.GetID(id);
                _logger.LogError(ex.Message, "Get data from data base");
                return Ok(res);
            }
            catch (ArgumentException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content("Name cannot be empty or less then zero");
            }
        }
        [HttpGet("chracter-name")]
        public async Task<IActionResult> CharacteName(string name)
        {
            try
            {
                var result = await cr.GetByName(name);
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                var res = await cdb.GetByName(name);
                _logger.LogError(ex.Message, "Get data from data base");
                return Ok(res);
            }
            catch (System.ArgumentNullException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content("Name don't exist");
            }
            catch (System.ArgumentException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content("Name cannot be empty");
            }
           
        }
        [HttpGet("chracter-name&status")]
        public async Task<IActionResult> CharacteNameandStatus(string name, string status)
        {
            try
            {
                var result = await cr.GetCharacterStatus(name, status);
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                var res = await cdb.GetCharacterStatus(name,status);
                _logger.LogError(ex.Message, "Get data from data base");
                return Ok(res);
            }
            catch (ArgumentException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content("Name cannot be empty");
            }
        }
        [HttpGet("chracter-species")]
        public async Task<IActionResult> CharacterSpicies(string species)
        {
            try
            {
                var result = await cr.GetCharacterBySpecies(species);
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                var res = await cdb.GetCharacterBySpecies(species);
                _logger.LogError(ex.Message, "Get data from data base");
                return Ok(res);
            }
            catch (ArgumentException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content("Name cannot be empty");
            }
        }
        [HttpGet("chracter-type")]
        public async Task<IActionResult> CharacterType(string type)
        {
            try
            {
                var result = await cr.GetCharacterByType(type);
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                var res = await cdb.GetCharacterByType(type);
                _logger.LogError(ex.Message, "Get data from data base");
                return Ok(res);
            }
            catch (ArgumentException ex)
            {
                _argumentLogger.LogWarning(ex.Message);
                return Content("Name cannot be empty");
            }
        }
        [HttpGet("chracter-name&gender")]
        public async Task<IActionResult> CharacteNameandGender(string name, string gender)
        {
            try
            {
                var result = await cr.GetCharacteGender(name, gender);
                _logger.LogInformation("Get data from API");
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                var res = await cdb.GetCharacteGender(name, gender);
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
