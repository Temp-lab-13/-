using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WATask1.DTO;
using WATask1.Repositori;

namespace WATask1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private IUserRepo _userRepo;
        public ValuesController(IUserRepo userRepo) 
        {
            _userRepo = userRepo;
        }

        [HttpPost(template:"Добавить пользователя")]
        public ActionResult AddUser(UserDto userDto)
        {
            _userRepo.AddUser(userDto);
            return Ok();
        }

        [HttpGet(template: "Поиск пользователя")]
        public ActionResult<bool> Exist(string email)
        {
            return Ok(_userRepo.Exist(email));
        }
    }
}
