using Microsoft.AspNetCore.Mvc;
using Asp.Net_Core_Web_API.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp.Net_Core_Web_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        string? ProductsString { get; set; }
        List<Products>? ProductsList = new List<Products>();

        public ProductsController()
        {
            ProductsString = System.IO.File.ReadAllText(@"C:\Development\.net\Asp.Net Core Web API\Asp.Net Core Web API\Models\Products.txt");
            ProductsList = JsonSerializer.Deserialize<List<Products>>(ProductsString);
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<string> Get()
        {
            ProductsString = await System.IO.File.ReadAllTextAsync(@"C:\Development\.net\Asp.Net Core Web API\Asp.Net Core Web API\Models\Products.txt");
            return ProductsString;
        }
        

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            Products products = ProductsList.FirstOrDefault(x=>x.ProductID== id);

            return JsonSerializer.Serialize(products);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            string[] strings = value.Split(',');
            Products products = new Products();
            products.ProductID= Convert.ToInt32(strings[0]);
            products.ProductName= strings[1];
            ProductsList.Add(products);

            ProductsString = JsonSerializer.Serialize(ProductsList); 
            System.IO.File.WriteAllText(@"C:\Development\.net\Asp.Net Core Web API\Asp.Net Core Web API\Models\Products.txt", ProductsString);

        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            Products products = ProductsList.FirstOrDefault(x => x.ProductID == id);

            products.ProductName = value;

            ProductsString = JsonSerializer.Serialize(ProductsList);
            System.IO.File.WriteAllText(@"C:\Development\.net\Asp.Net Core Web API\Asp.Net Core Web API\Models\Products.txt", ProductsString);

        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Products products = ProductsList.FirstOrDefault(x => x.ProductID == id);
            ProductsList.Remove(products);
            ProductsString = JsonSerializer.Serialize(ProductsList);
            System.IO.File.WriteAllText(@"C:\Development\.net\Asp.Net Core Web API\Asp.Net Core Web API\Models\Products.txt", ProductsString);
        }

        
    }
}
