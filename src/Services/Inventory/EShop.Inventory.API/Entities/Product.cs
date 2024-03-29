﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace EShop.Inventory.API.Entities
{
    public class Product : BaseEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("category")]
        [JsonProperty("category")]
        public string Category { get; set; }

        [BsonElement("summary")]
        public string Summary { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("image_file")]
        [JsonProperty("image_file")]
        public string Image { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }
    }
}
