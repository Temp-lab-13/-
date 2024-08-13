using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WATask2.DTO;
using WATask2.Repositiri;

namespace WATask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private ILiberyRepo _liberyRepo;

        public LibraryController(ILiberyRepo liberyRepo)
        {
            _liberyRepo = liberyRepo;
        }

        [HttpPost(template:"Добавить автора")]
        public ActionResult AddAutor(AuthorDto author)
        {
            _liberyRepo.AddAuthor(author);
            return Ok();
        }

        [HttpGet(template: "Найти автора")]
        public ActionResult<IEnumerable<AuthorDto>> GetAutor() 
        {
            return Ok(_liberyRepo.GetAuthors()); 
        }

        [HttpPost(template: "Добавить Книгу")]
        public ActionResult AddBook(BookDto bookDto)
        {
            _liberyRepo.AddBook(bookDto);
            return Ok();
        }

        [HttpGet(template: "Найти книгу")]
        public ActionResult<IEnumerable<BookDto>> GetBook()
        {
            return Ok(_liberyRepo.GetBooks);
        }

        [HttpGet(template: "Проверить книгу")]
        public ActionResult<bool> ChekBook(Guid bookId)
        {
            return Ok(_liberyRepo.CheckBook(bookId));
        }

    }
}
