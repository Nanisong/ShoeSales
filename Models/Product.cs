using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ShoeSales.Models
{
    //[BsonIgnoreExtraElements] 
    public class Product
    {
        [BsonId]
        public object MongoId { get; set; }
        [BsonElement("id")]
        public int Id { get; set; }
        [BsonElement("categoryId")]
        public int CategoryId { get; set; }
        [BsonElement("brand")]
        public string Brand { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("location")]
        public string? Location { get; set; }
        [BsonElement("size")]
        public Decimal Size { get; set; }
        [BsonElement("price")]
        public Decimal Price { get; set; }
        [BsonElement("isAvailable")]
        public bool IsAvailable { get; set; }
        //Anything that the driver finds when it's serializing or de-serializing that it doesn't recognize as one of the properties we have already specified it will throw it into this object array as a key-value pair. or we can use [BsonIgnoreExtraElements] above the Class name 
        //[BsonExtraElements]
        //public object[] Bucket { get; set; }

        [JsonIgnore]
        public virtual Category? Category { get; set; }
    }
}

