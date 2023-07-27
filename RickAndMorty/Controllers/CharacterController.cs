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

namespace RickAndMorty.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class CharacteerController : ControllerBase
    {
        
        private ICharacterRequester cr;
        public CharacteerController(ICharacterRequester cr)
        {
            this.cr = cr;
        }

        [HttpPost("multiple-characters")]
        public async Task<IActionResult> ListSomeCharacters(List<int> list)
        {
            try
            {
                var result = await cr.GetByIDlist(list);
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

        [HttpGet("all-characters")]
        public async Task<IActionResult> AllCharacters()
        {
            try
            {
                var result = await cr.GetAll();
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                //return data from db
                return Content(ex.Message);
            }
        }

        [HttpGet("character-id")]
        public async Task<IActionResult> CharacteID(int id)
        {
            try
            {
                var result = await cr.GetID(id);
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
        [HttpGet("chracter-name")]
        public async Task<IActionResult> CharacteName(string name)
        {
            try
            {
                var result = await cr.GetByName(name);
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
        [HttpGet("chracter-name&status")]
        public async Task<IActionResult> CharacteNameandStatus(string name, string status)
        {
            try
            {
                var result = await cr.GetCharacterStatus(name, status);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                //return data from db
                return Content(ex.Message);
            }
            catch (ArgumentException ex)
            {
                // return text or give able to write new info
                return Content(ex.Message);
            }
        }
        [HttpGet("chracter-species")]
        public async Task<IActionResult> CharacterSpicies(string species)
        {
            try
            {
                var result = await cr.GetCharacterBySpecies(species);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                //return data from db
                return Content(ex.Message);
            }
            catch (ArgumentException ex)
            {
                // return text or give able to write new info
                return Content(ex.Message);
            }
        }
        [HttpGet("chracter-type")]
        public async Task<IActionResult> CharacterType(string type)
        {
            try
            {
                var result = await cr.GetCharacterByType(type);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                //return data from db
                return Content(ex.Message);
            }
            catch (ArgumentException ex)
            {
                // return text or give able to write new info
                return Content(ex.Message);
            }
        }
        [HttpGet("chracter-name&gender")]
        public async Task<IActionResult> CharacteNameandGender(string name, string gender)
        {
            try
            {
                var result = await cr.GetCharacteGender(name, gender);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                //return data from db
                return Content(ex.Message);
            }
            catch (ArgumentException ex)
            {
                // return text or give able to write new info
                return Content(ex.Message);
            }
        }


    }
}
