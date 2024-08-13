using Microsoft.AspNetCore.Mvc;

namespace WATaskTwo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MathController : ControllerBase
    {
        /* // Оин контроллер не может исполнить два метода с параметрами `Name`, даже если имена раные.
        [HttpGet(Name = "Вычисление квадрата корня")]
        public int Scuaere(int x) 
        {
            return x * x;
        }
        */

        // Но это возможно с параметром `template`
        /*
        [HttpGet(template: "Вычисление квадрата корня")]
        public int Scuaere(int x)
        {
            return x * x;
        }

        [HttpGet(template: "Сложение")]
        public int Sum(int x, int y)
        {
            return x + y;
        }
        */

        [HttpGet(template: "Divide")]
        public ActionResult<int> Divide(int x, int y) // Нужно для мфгкого возращения ошибок
        {
            try
            {
                var result = x / y;
                return Ok(result);
            }
            catch (DivideByZeroException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) 
            { 
                return StatusCode(500, ex.Message); 
            }
            

        }

    }
}
