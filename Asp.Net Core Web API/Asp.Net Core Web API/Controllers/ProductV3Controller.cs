using Asp.Net_Core_Web_API.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp.Net_Core_Web_API.Controllers
{
    [Route("api/v3/[controller]")]
    [ApiController]
    [ApiVersion("3.0")]
    public class ProductV3Controller : ControllerBase
    {
                
        private IProduct product;
        private IConfiguration configuration;

        public ProductV3Controller(IProduct product, IConfiguration configuration)
        {            
            this.product= product;
            this.configuration= configuration;
        }

        // GET: api/<ProductV3Controller>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                string strProduct = product.GetProducts();
                return string.IsNullOrEmpty(strProduct) ? NotFound() : Ok(strProduct);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET api/<ProductV3Controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                string strProduct = product.GetProductById(id);
                return string.IsNullOrEmpty(strProduct) ? NotFound() : Ok(strProduct);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/<ProductV3Controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            product.AddProducts(value);
        }

        // PUT api/<ProductV3Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            product.UpdateProducts(id, value);
        }

        // DELETE api/<ProductV3Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            product.DeleteProducts(id);
        }
    }
}
