using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WATask.Utilit;

namespace WATask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly Ifibo _ifibo;

        public ValuesController(Ifibo ifibo)
        {
            _ifibo = ifibo;
        }

        [HttpGet(template:"fi")]
        public ActionResult<int> Fi(int x)
        {
            return Ok(_ifibo.fibochi(x));
        }
    }
}
