using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace load_balancer.Models
{
    public class Server
    {
        public const string CollectionName = "api-servers";

        public Server(string address)
        {
            Address = address;
        }
        
        [BsonId]
        public ObjectId Id { get; set; }

        public string Address { get; set; }

        public uint Ping { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public int Load { get; set; } = 0;

        public int MaxLoad { get; set; } = 1000;

        public bool LoadBool => (MaxLoad - Load) > 1;
        
        public List<string> Works { get; set; } = new List<string>();
        
        public DateTime Last { get; set; } = DateTime.Now;

        public void AddWork(string work)
        {
            Works.Add(work);
            Load++;
            Last = DateTime.Now;
        }

        public void RemoveWork(string work)
        {
            Works.Remove(work);
            Load--;
        }
    }
}