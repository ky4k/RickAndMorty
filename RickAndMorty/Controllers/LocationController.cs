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

namespace RickAndMorty.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class LocationController : Controller
    {
        private ILocationRequester lr;
        public LocationController(ILocationRequester lr)
        {
            this.lr = lr;
        }

        [HttpGet("all-locations")]
        public async Task<IActionResult> AllLocations()
        {
            try
            {
                var result = await lr.GetAll();
                return Ok(result);
            }
            catch(HttpRequestException ex)
            {
                //return data from db
                return Content(ex.Message);
            }
        }
        [HttpPost("multiple-locations")]
        public async Task<IActionResult> MultiplyLocations(List<int> list)
        {
            try
            {
                var result = await lr.GetByIDlist(list);
                return Ok(result);
            }
            catch(HttpRequestException ex)
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
        [HttpGet("location-id")]
        public async Task<IActionResult> LocationID(int id)
        {
            try
            {
                var result = await lr.GetByID(id);
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
        [HttpGet("location-type")]
        public async Task<IActionResult> LocationType(string type)
        {
            try
            {
                var result = await lr.GetByType(type);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                //return data from db
                return Content(ex.Message);
            }
            catch (ArgumentException ex)
            {
                // return text 
                return Content(ex.Message);
            }
        }
        [HttpGet("location-demension")]
        public async Task<IActionResult> LocationDemesion(string demension)
        {
            try
            {
                var result = await lr.GetByDimension(demension);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                //return data from db
                return Content(ex.Message);
            }
            catch (ArgumentException ex)
            {
                // return text 
                return Content(ex.Message);
            }
        }
        [HttpGet("location-name")]
        public async Task<IActionResult> LocationName(string name)
        {
            try
            {
                var result = await lr.GetByName(name);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                //return data from db
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
