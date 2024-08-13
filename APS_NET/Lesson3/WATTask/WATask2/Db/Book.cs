namespace WATask2.Db
{
    public class Book
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public Guid? AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
