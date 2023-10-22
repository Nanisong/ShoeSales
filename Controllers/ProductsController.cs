using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeSales.Models;
using MongoDB.Bson;
using MongoDB.Driver;

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
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetFromMongo()
        {
            //var client = new MongoClient("mongodb+srv://lani:Haj1menoMongoru@cluster0.2motmkt.mongodb.net/?retryWrites=true&w=majority");
            //var database = client.GetDatabase("magasin");
            //var collection = database.GetCollection<Product>("product");

            

            //var products = collection.Find(s => s.Brand == "New Balance").ToList();
            var products = shoesCollection.Find(s => s.Brand == "New Balance").ToList();
            if (products.Count == 0)
            { 
                return NotFound();
            }
            return Ok(products);
        }

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
