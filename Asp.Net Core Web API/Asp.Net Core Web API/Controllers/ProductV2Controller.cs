using Asp.Net_Core_Web_API.Interface;
using Asp.Net_Core_Web_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp.Net_Core_Web_API.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class ProductV2Controller : ControllerBase
    {
        string? ProductsString { get; set; }
        List<Products>? ProductsList = new List<Products>();
        private readonly IMessage _message;
        public ProductV2Controller(IMessage message )
        {
            ProductsString = System.IO.File.ReadAllText(@"C:\Development\.net\Asp.Net Core Web API\Asp.Net Core Web API\Models\Products.txt");
            ProductsList = JsonSerializer.Deserialize<List<Products>>(ProductsString);
            _message = message;
        }

        // GET: api/<ProductV2Controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                ProductsString = await System.IO.File.ReadAllTextAsync(@"C:\Development\.net\Asp.Net Core Web API\Asp.Net Core Web API\Models\Products.txt");
                return ProductsString == null ? NotFound() : Ok(ProductsString);
                
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET api/<ProductV2Controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Products products = ProductsList.FirstOrDefault(x => x.ProductID == id);
                ProductsString = JsonSerializer.Serialize(products);
                return products == null? NotFound(): Ok(ProductsString);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // POST api/<ProductV2Controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            string[] strings = value.Split(',');
            Products products = new Products();
            products.ProductID = Convert.ToInt32(strings[0]);
            products.ProductName = strings[1];
            ProductsList.Add(products);
            SetData(ProductsList);
        }

        // PUT api/<ProductV2Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            Products products = ProductsList.FirstOrDefault(x => x.ProductID == id);

            products.ProductName = value;

            SetData(ProductsList);
        }

        // DELETE api/<ProductV2Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Products products = ProductsList.FirstOrDefault(x => x.ProductID == id);
            ProductsList.Remove(products);
            SetData(ProductsList);
        }

        private async Task<List<Products>> GetData()
        {
            ProductsString = await System.IO.File.ReadAllTextAsync(@"C:\Development\.net\Asp.Net Core Web API\Asp.Net Core Web API\Models\Products.txt");
            ProductsList = JsonSerializer.Deserialize<List<Products>>(ProductsString);
            return ProductsList;
        }
        private void SetData(List<Products>? ProductsList)
        {
            ProductsString = JsonSerializer.Serialize(ProductsList);
            System.IO.File.WriteAllText(@"C:\Development\.net\Asp.Net Core Web API\Asp.Net Core Web API\Models\Products.txt", ProductsString);
        }

        private void AddMessage()
        {
            _message.WriteMessage("DI");
        }
    }
}
