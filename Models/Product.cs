using System.Text.Json.Serialization;

namespace ShoeSales.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
        public Decimal Size { get; set; }
        public Decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        [JsonIgnore]
        public virtual Category? Category { get; set; }
    }
}

