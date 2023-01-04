using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace IE307.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string price { get; set; }
        public string type { get; set; }
        public string material { get; set; }
        public int sold { get; set; }
        public Boolean deleted { get; set; }

        [BsonElement("createdAt")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime createdAt { get; set; }

        [BsonElement("updatedAt")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime updatedAt { get; set; }

        [BsonElement("__v")]
        public int __v { get; set; }

    }
}
