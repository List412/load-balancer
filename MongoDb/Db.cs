using load_balancer.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace load_balancer.MongoDb
{
    public class Db
    {
        private const string ConnectionString = "mongodb+srv://list412:List_412@distributed-system-ejqle.azure.mongodb.net/test?retryWrites=true";
        private MongoClient _client;
        private IMongoDatabase _db;
        public IMongoCollection<Server> Collection { get; set; }

        public Db()
        {
            _client = new MongoClient(ConnectionString);
            _db = _client.GetDatabase("distributed");
            Collection = _db.GetCollection<Server>(Server.CollectionName);
        }


        public ObjectId Add(Server server)
        {
            Collection.InsertOne(server);
            return server.Id;
        }
        
        //TODO Get, GetAll, Delete
    }
}