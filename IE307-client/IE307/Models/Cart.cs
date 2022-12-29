using System;
using System.Collections.Generic;
using System.Text;
using IE307.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IE307.Models
{
    public class Item 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string ProductId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string price { get; set; }
        public string type { get; set; }
        public string material { get; set; }

        [BsonElement("createdAt")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime createdAt { get; set; }

        [BsonElement("updatedAt")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime updatedAt { get; set; }

        [BsonElement("__v")]
        public int __v { get; set; }
    }

    public class CartItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string itemId { get; set; }
        [BsonElement("item")]
        public Item item { get; set; }
        [BsonElement("quantity")]
        public int quantity { get; set; }
        [BsonElement("price")]
        public int price { get; set; }  
    }

    public class Cart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string cartID { get; set; }

        [BsonElement("userID")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string userID { get; set; }
        [BsonElement("totalQuantity")]
        public int totalquantity { get; set; }
        [BsonElement("totalPrice")]
        public long totalPrice { get; set; }
        [BsonElement("items")]
        public List<CartItem> items { get; set; }
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
