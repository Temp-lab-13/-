using WATask2.DTO;

namespace WATask2.Repositiri
{
    public interface ILiberyRepo
    {
        public void AddAuthor(AuthorDto author);
        public void AddBook(BookDto book);
        public IEnumerable<BookDto> GetBooks();
        public IEnumerable<AuthorDto> GetAuthors();
        public bool CheckBook(Guid bookId);
    }
}
