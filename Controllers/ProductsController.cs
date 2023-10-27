using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeSales.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using static MongoDB.Driver.WriteConcern;

namespace ShoeSales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //private readonly ShopContext _shopContext;
        private IMongoCollection<Product> shoesCollection;

        public ProductsController(IMongoClient client)
        {
            var database = client.GetDatabase("magasin");
            shoesCollection = database.GetCollection<Product>("product");
        }
        //public ProductsController(ShopContext shopContext)
        //{
        //    _shopContext = shopContext;
        //    _shopContext.Database.EnsureCreated();
        //}
        //[HttpGet]
        ////Previeous Code1: get all products 
        //public IEnumerable<Product> GetAllProducts()
        //{
        //    return _shopContext.Products.ToArray();
        //}

        // Previeous Code2: Before async
        //public ActionResult<IEnumerable<Product>> GetAllProducts()
        //{
        //    var products = _shopContext.Products.ToArray();
        //    if (!products.Any())
        //    {
        //        return NotFound();
        //    }
        //    return Ok(products);
        //}

        // Making the API asynchronous
        //public async Task<ActionResult> GetAllProducts()
        //{   
        //    var products = await _shopContext.Products.ToArrayAsync();
        //    //Sleep 20 Seconds for test
        //    Thread.Sleep(20000);
        //    if (!products.Any())
        //    {
        //        return NotFound();
        //    }
        //    return Ok(products); 
        //}
        //[Route("api/[controller]")]
        [HttpGet]
        public async Task<ActionResult> GetAllProduct()
        {
            //To find all documents, pass an empty filter to Find()
            var fileter = Builders<Product>.Filter.Empty;
            var product = await shoesCollection.Find(fileter).ToListAsync();
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpGet("byid/{id}")]
        public async Task<ActionResult> GetProductByID(int id)
        {
            //return either the first product that matches the filter, or null if none is found. 
            var product = await shoesCollection.Find(s => s.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [Route("api/[controller]/byprice")]
        [HttpGet]
        public async Task<ActionResult> GetProductByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var product = await shoesCollection.Find(s => s.Price.CompareTo(maxPrice) <= 0 && s.Price.CompareTo(minPrice) >= 0).ToListAsync();
            if (product.Count == 0)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpGet("bybrand/{brand}")]
        public async Task<ActionResult> GetAListOfItems(string brand)
        {   //Get brand name : New Balance
            var products =await shoesCollection.Find(s => s.Brand == brand).ToListAsync();
            if (products.Count == 0)
            {   //404
                return NotFound();
            }
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> PostProduct(Product product)
        {
            await shoesCollection.InsertOneAsync(product);
            return CreatedAtAction(nameof(GetAllProduct), new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<ActionResult> EditProduct(int id, decimal newPrice)
        {
            var existingProduct = await shoesCollection.Find(s => s.Id == id).FirstOrDefaultAsync();
            if (existingProduct == null)
            {
                return NotFound();
            }
            else 
            {   // Can only update the Price with ID
                var filter = Builders<Product>.Filter.Eq(s => s.Id, id);
                var update = Builders<Product>.Update.Set(s => s.Price, newPrice);
                await shoesCollection.UpdateOneAsync(filter, update);
                return NoContent();
            }
        }

        //public ActionResult<IEnumerable<Product>> GetFromMongoMotomoto()
        //{
        //    var products = shoesCollection.Find(s => s.Brand == "New Balance").ToList();
        //    if (products.Count == 0)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(products);
        //}

        //[Route("api/[controller]")]
        //[HttpGet]
        //public ActionResult GetProduct(int id)
        //{
        //    var product = _shopContext.Products.Find(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(product);
        //}

    }
}
