using Asp.Net_Core_Web_API.Interface;
using Asp.Net_Core_Web_API.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp.Net_Core_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductV4Controller : ControllerBase
    {
        private readonly IDbContext dbContext;
        public ProductV4Controller(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
     
        // GET: api/<ProductV3Controller>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                ProductDTO productDTO = new ProductDTO();
                List<ProductDTO> products = new List<ProductDTO>();
                DbConnection dbConnection = dbContext.GetDbConnection(dbContext.GetConnectionString());
                dbConnection.Open();
                products = dbConnection.Query<ProductDTO>("select * from products").ToList();
                string ProductsJson = JsonSerializer.Serialize(products);
                return string.IsNullOrEmpty(ProductsJson) ? NotFound() : Ok(ProductsJson);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET api/<ProductV4Controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                ProductDTO? product = new ProductDTO();
                DbConnection dbConnection = dbContext.GetDbConnection(dbContext.GetConnectionString());
                dbConnection.Open();
                product = dbConnection.Query<ProductDTO>("select * from products where productid = "+ id).FirstOrDefault();
                string ProductsJson = JsonSerializer.Serialize(product);
                return string.IsNullOrEmpty(ProductsJson) ? NotFound() : Ok(ProductsJson);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/<ProductV4Controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            try
            {
                ProductDTO? product = new ProductDTO();
                product.ProductName = value;
                List<ProductDTO> products = new List<ProductDTO>();
                products.Add(product);
                DbConnection dbConnection = dbContext.GetDbConnection(dbContext.GetConnectionString());
                dbConnection.Open();
                dbConnection.Execute("insert into products (productname) values('" + value + "')");
            }
            catch (Exception)
            {
                BadRequest();
            }
            
        }

        // PUT api/<ProductV4Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            try
            {
                ProductDTO? product = new ProductDTO();
                product.ProductName = value;
                List<ProductDTO> products = new List<ProductDTO>();
                products.Add(product);
                DbConnection dbConnection = dbContext.GetDbConnection(dbContext.GetConnectionString());
                dbConnection.Open();
                dbConnection.Execute("update products set productname = '" + value + "' where productid = " + id);
            }
            catch (Exception)
            {
                BadRequest();
            }
        }

        // DELETE api/<ProductV4Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                DbConnection dbConnection = dbContext.GetDbConnection(dbContext.GetConnectionString());
                dbConnection.Open();
                dbConnection.Execute("delete from products where productid = " + id);
            }
            catch (Exception)
            {
                BadRequest();
            }
            
        }
    }
}
