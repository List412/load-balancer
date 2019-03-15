using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using load_balancer.Models;
using load_balancer.MongoDb;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace load_balancer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("status")]
//    [ApiController]
    public class ValuesController : ControllerBase
    {

        private Db _db;
        
        public ValuesController()
        {
            _db = new Db();
        }
        
        // GET status
        [HttpGet]
        public List<Server> Get()
        {
            return _db.GetAll();
        }

        // GET status/5
        [HttpGet("{id}")]
        public Server Get(string id)
        {
            return _db.Get(new ObjectId(id));
        }


        // POST status
        [HttpPost]
        public void Post([FromBody] string value)
        {
            
        }

        // PUT status/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE status/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
