using AutoMapper;
using System.Net;
using WATask2.Db;
using WATask2.DTO;

namespace WATask2.Repositiri
{
    public class LiberyRepo : ILiberyRepo
    {
        private IMapper _mapper;
        private AppDbContext _appDbContext;
        public LiberyRepo(IMapper mapper, AppDbContext appDbContext) 
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }
        public void AddAuthor(AuthorDto author)
        {
            using (_appDbContext)
            {
                _appDbContext.Authors.Add(_mapper.Map<Author>(author));
                _appDbContext.SaveChanges();
            }
        }

        public void AddBook(BookDto book)
        {
            using (_appDbContext)
            {
                _appDbContext.Books.Add(_mapper.Map<Book>(book));
                _appDbContext.SaveChanges();
            }
        }

        public bool CheckBook(Guid bookId)
        {
            using (_appDbContext) 
            {
                return _appDbContext.Books.Any(x => x.Id ==(bookId));
            }
        }

        public IEnumerable<AuthorDto> GetAuthors()
        {
            using (_appDbContext)
            {
                return _appDbContext.Authors.Select(_mapper.Map<AuthorDto>).ToList();
            }
        }

        public IEnumerable<BookDto> GetBooks()
        {
            using (_appDbContext)
            {
                return _appDbContext.Books.Select(_mapper.Map<BookDto>).ToList();
            }
        }
    }
}
