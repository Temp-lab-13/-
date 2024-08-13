namespace WATask2.Db
{
    public class Author
    {
        public string? Name { get; set; }
        public Guid? Id { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
