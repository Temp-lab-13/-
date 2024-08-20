using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using WATask.IAbstract;
using WATask.Models;
using WATask.Models.Context;
using WATask.Models.DTO;
using WATask.Models.ModelAnswer;

namespace WATask.Controllers
{
    // Домашняя задача:
    // №1. Доработайте контроллер, реализовав в нем метод возврата CSV-файла с товарами;
    // №2. Доработайте контроллер реализовав статичный файл со статистикой работы кэш;
    // №3. Сделайте файл доступным по ссылке;
    // №4. Перенесите строку подключения для работы с базой данных в конфигурационный файл приложения.

    [Route("Product/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IServiceProduct service;

        public ProductController(IServiceProduct service)
        {
            this.service = service;
        }

        [HttpGet(template: "GetProduct")] // Получение продукта
        public IActionResult GetProduct() 
        {
            var products = service.GetProducts();
            return Ok(products);
        }

        [HttpGet(template: "GetCategory")] // Получение категорий
        public IActionResult GetCategory()
        {
            var categorys = service.GetCategories();
            return Ok(categorys);
        }

        [HttpPost(template: ("PostCategory"))] // Добавление новой категории. В ручную.
        public IActionResult AddCategory([FromQuery] string categoryName, string descript)
        {
            try
            {
                var category = new CategoryDto() { Name = categoryName, Descript = descript };
                service.AddCategory(category);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost(template: "PostProduct")] // Добавление продукта
        public IActionResult Post([FromQuery] string name, string descript, int categorId, int price)
        {
            try
            {
                var product = new ProductDto() { Name = name, Descript = descript, Price = price, CategoriId = categorId };
                service.AddProduct(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdatePrice")] // Добавление или изменение цены.
        public IActionResult UpPrise([FromQuery] string nameProduct, int price)
        {
            try
            { 
                var product = new ProductDto() { Name = nameProduct, Price = price };
                service.UpPrise(product);
                return Ok("Цена обновлена!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete(template:"DelProduct")]
        public IActionResult DeletProduct(string nameProduct)
        {
            try
            {
                var product = new ProductDto() { Name = nameProduct};
                service.DeletProduct(product);
                return Ok("Продукт удалён.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete(template: "DelCategody")]
        public IActionResult DeletCategory(string categoryName)
        {
            try
            {
                var category = new CategoryDto() { Name = categoryName};
                service.DeletCategory(category);
                return Ok("Категория продуктов удалена.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(template: "GetFileCSV")]
        public ActionResult<string> GetFileCSV()
        {
            try
            {
                string path = $"https://{Request.Host.ToString()}/static/{service.GetProductCsvUrl()}";

                return path;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(template: "DownloadFileCSV")]
        public FileContentResult? DownloadFileCSV()
        {
            try
            {
                return File(new System.Text.UTF8Encoding().GetBytes(service.GetProductCsv()), "text/csv", "report.csv");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet(template: "GetStatictic")]
        public ActionResult<string> GetStatictic()
        {
            try
            {
                string path = $"https://{Request.Host.ToString()}/static/{service.GetStatistic()}";
                return path;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
