using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WATask.Models;
using WATask.Models.Abstract;
using WATask.Models.Dto;

namespace WATask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _repo;

        public ProductController(IProductRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("GetProduct")]
        public IActionResult GetProduct() 
        {
            var product = _repo.GetProducts();
            return Ok(product);

        }

        [HttpGet("GetCatalog")]
        public IActionResult GetCatalog()
        {
            var categoris = _repo.GetCategories();
            return Ok(categoris);

        }

        [HttpPost("PostProduct")]
        public IActionResult AddProduct([FromQuery] ProductDto productDto)
        {
            var resalt = _repo.AddProduct(productDto);
            return Ok(resalt);
        }

        [HttpPost("PostCatalog")]
        public IActionResult AddCatalog([FromQuery] CatalogDto catalogDto)
        {
            var resalt = _repo.AddCatalog(catalogDto);
            return Ok(resalt);
        }
    }
}
