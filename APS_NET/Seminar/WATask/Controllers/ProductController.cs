using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WATask.Models;

namespace WATask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("GetProduct")]
        public IActionResult Get() 
        {
            try
            {
                using (var context = new ProductContext()) 
                {
                    var product = context.Products.Select(x => new Product() 
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Descript = x.Descript
                    });
                    return Ok(product);
                }
            }
            catch (Exception ex) 
            { 
                return StatusCode(500, ex.Message); 
            }
        }

        [HttpPost("PostProduct")]
        public IActionResult Post([FromQuery] string name, string descript, int categorId, int price)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if(!context.Products.Any(x => x.Name.ToLower().Equals(name)))
                    {
                        context.Add(new Product()
                        {
                            Name = name,
                            Descript = descript,
                            Price = price,
                            CategoriId = categorId
                        });
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(409);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
