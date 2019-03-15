using load_balancer.Models;
using Microsoft.EntityFrameworkCore;

namespace load_balancer.MongoDb
{
    public class ContextMongo : DbContext
    {
        public Db Db { get; set; }

        public ContextMongo()
        {
            Db = new Db();
        }

    }
}