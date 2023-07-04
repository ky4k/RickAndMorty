using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Interfaces;
using System.Net.Http;

namespace RickAndMorty.Controllers
{
    [ApiController]
    [Route("api/v1")]

    public class ManageController : Controller
    {
        private IDataOperation db;
        public ManageController(IDataOperation _db)
        {
            db = _db;
        }
        [HttpPost("updatedb")]
        public async Task UpdateDB()
        {
            await db.UpdateData();

        }
    }
}
