using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShoeSales.Models
{
    //[BsonIgnoreExtraElements] 
    public class Product
    {
        //[BsonId]
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId? MongoId { get; set; }
        [BsonElement("id")]
        [Required]
        public int Id { get; set; }
        [BsonElement("categoryId")]
        [Required]
        public int CategoryId { get; set; }
        [BsonElement("brand")]
        [Required]
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

