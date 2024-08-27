namespace WATask2.Models
{
    public class Product
    {
        public int? CategoriId { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Price { get; set; }
        public string? Descript { get; set; }
        public virtual Category? Category { get; set; }
        //public virtual List<Storage>? Stores { get; set; }

    }
}
