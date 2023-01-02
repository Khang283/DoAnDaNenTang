using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IE307.Models
{
   
    public class Favorite
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string itemId { get; set; }
        [BsonElement("item")]
        public Product item { get; set; }
    }
    public class Account
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }    
        public string role { get; set; }    
        public string email { get; set; }   
        public string phone { get; set; }
        public string address { get; set; }
        [BsonElement("favorite")]
        public List<Favorite> favorite { get; set; }   
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
