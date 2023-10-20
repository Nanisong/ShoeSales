using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        //[HttpGet]
        ////(Previous) get all products 
        //public IEnumerable<Product> GetAllProducts()
        //{
        //    return _shopContext.Products.ToArray();
        //}
        [HttpGet]
        // Previeous Code: Before async
        //public ActionResult<IEnumerable<Product>> GetAllProducts()
        //{
        //    var products = _shopContext.Products.ToArray();
        //    if (!products.Any())
        //    {
        //        return NotFound();
        //    }
        //    return Ok(products);
        //}

        //Topic 4: Making the API asynchronous
        public async Task<ActionResult> GetAllProducts()
        {
            var products = await _shopContext.Products.ToArrayAsync();
            if (!products.Any())
            {
                return NotFound();
            }
            return Ok(products); 
        }

        [Route("api/[controller]")]
        [HttpGet]
        public ActionResult GetProduct(int id)
        {
            var product = _shopContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

    }
}
