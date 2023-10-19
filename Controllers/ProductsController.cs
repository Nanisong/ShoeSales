using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoeSales.Models;

namespace ShoeSales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _shopContext;
        public ProductsController(ShopContext shopContext)
        {
            _shopContext = shopContext;
            _shopContext.Database.EnsureCreated();
        }
        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            return _shopContext.Products.ToArray();
        }
    }
}
