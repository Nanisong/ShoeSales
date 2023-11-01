namespace ShoeSalesAPI.Models
{
    public class ProductParametersQuery : QueryParameters
    {
        //MinPrice
        public decimal? MinPrice { get; set; }
        //MaxPrice
        public decimal? MaxPrice { get; set; }

        public string Brand { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SearchTerm { get; set; } = string.Empty;
    }
}
