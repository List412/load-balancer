using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using load_balancer.Models;
using load_balancer.MongoDb;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace load_balancer.Controllers
{
    [Route("api/{*url}")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        private IMongoCollection<Server> db;
        private HttpClient _http;
        public ServerController()
        {
            db = new Db().Collection;
            _http = new HttpClient();
        }
        
        // GET api/any
        [HttpGet]
        public async Task<string> Get()
        {
            var server = GetServer();
            var newPath = server.Address + Request.Path;
            server.AddWork(newPath);
            var result = await _http.GetAsync(newPath);
            server.RemoveWork(newPath);
            return result.Content.ReadAsStringAsync().Result.ToJson();
        }

        // POST api/any
        [HttpPost]
        public async Task<string> Post([FromBody] string value)
        {
            var server = GetServer();
            var newPath = server.Address + Request.Path;
            server.AddWork(newPath);
            var result = await _http.PostAsJsonAsync(newPath, value);
            server.RemoveWork(newPath);
            return result.Content.ReadAsStringAsync().Result.ToJson();
        }

        // PUT api/any/5
        [HttpPut()]
        public void Put([FromBody] string value)
        {
            // TODO preg math last number from Request.Path and send it to real api
        }

        // DELETE api/any/5
        [HttpDelete()]
        public void Delete()
        {
        }

        private Server GetServer()
        {
            var all = db.Find(x => x.IsActive).ToList().OrderBy(x => x.Load);
            // TODO check for empty all
            if (all.Count() > 0)
                return all.First();
            else
                return new Server("https://127.0.0.1:5001");
        }
    }
}