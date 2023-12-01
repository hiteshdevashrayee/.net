using ASP.NET_Core_Web_API_II.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP.NET_Core_Web_API_II.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductList productList;
        private readonly IUser user;
        private readonly IApplication application;

        public ProductsController( IProductList productList, IUser user, IApplication application)
        {
            this.productList = productList;
            this.user = user;
            this.application = application;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
              string Ids = "ApplicationId: " + application.GetApplicationId() +  "\n UserId: " + user.GetUserId() + "\n ProductId: " + productList.GetProductId();
                
              return string.IsNullOrEmpty(Ids) ? NotFound() : Ok(Ids);
                
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
