using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppleController : ControllerBase
    {
        public DB Db { get; }

        public AppleController(DB db)
        {
            Db = db;
        }

        // GET: api/<AppleController>
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new AppleQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/<AppleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new AppleQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/<AppleController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ApplePost body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/<AppleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] ApplePost body)
        {
            await Db.Connection.OpenAsync();
            var query = new AppleQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.Type = body.Type;
            result.Model = body.Model;
            result.Info = body.Info;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/<AppleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new AppleQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }

        // DELETE api/<AppleController>
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await Db.Connection.OpenAsync();
            var query = new AppleQuery(Db);
            await query.DeleteAllAsync();
            return new OkResult();
        }



    }
}
