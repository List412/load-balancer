using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using load_balancer.Models;
using load_balancer.MongoDb;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace load_balancer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
            _http.DefaultRequestHeaders.Add("Access-Control-Allow-Origin","*");
        }
        
        // GET api/any
        [HttpGet]
        public async Task<string> Get()
        {
            var server = GetServer();
            var newPath = server.Address + Request.Path;
            
            server.AddWork(newPath);

            if (Request.Path == "/api/server")
            {
                server.RemoveWork(newPath);
                return server.Address.ToJson();
            }
            
            var result = await _http.GetAsync(newPath);
            server.RemoveWork(newPath);
            return result.Content.ReadAsStringAsync().Result.ToJson();
        }

        // POST api/any
        [HttpPost]
        public async Task<string> Post(PictureJson file)
        {
            var server = GetServer();
            var newPath = server.Address + Request.Path;
            server.AddWork(newPath);

            var result = _http.PostAsJsonAsync(newPath, file);
            server.RemoveWork(newPath);
            return result.ToJson();
        }

        // PUT api/any/5
        [HttpPut()]
        public void Put([FromBody] string value)
        {
            
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

public class PictureJson
{
    public string Name { get; set; }
    public string File { get; set; }
}