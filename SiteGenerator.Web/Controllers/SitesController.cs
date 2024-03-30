using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SiteGenerator.Domain.Entities;
using SiteGenerator.Infrastructure.Data;

namespace SiteGenerator.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitesController(MongoDbService mongoDbService) : ControllerBase
    {
        private readonly IMongoCollection<Website>? _websites = mongoDbService.Database?.GetCollection<Website>("websites");

        [HttpGet]
        public async Task<IEnumerable<Website>> Get()
        {
            return await _websites.Find(FilterDefinition<Website>.Empty).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Website?>> GetById(string id)
        {
            var filter = Builders<Website>.Filter.Eq(x => x.Id, id);
            var test = await _websites.FindAsync(filter);
            var item = test.FirstOrDefault();
            return test is not null ? Ok(test) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create(string name)
        {
            var test = new Website() { Name = name };
            await _websites.InsertOneAsync(test);
            return CreatedAtAction(nameof(GetById), new { id = test.Id }, test);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Website test)
        {
            var filter = Builders<Website>.Filter.Eq(x => x.Id, test.Id);
            //var update = Builders<Test>.Update
            //    .Set(x => x.Name, test.Name);
            //await _test.UpdateOneAsync(filter, update);
            await _websites.ReplaceOneAsync(filter, test);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var filter = Builders<Website>.Filter.Eq(x => x.Id, id);
            await _websites.DeleteOneAsync(filter);
            return Ok();
        }
    }
}
