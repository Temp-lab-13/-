namespace WATask.Models
{
    public class Product : BModel
    {
        public int CategoriId { get; set; }
        public int? Price { get; set; }
        public virtual Category? Category { get; set; }
        public virtual List<Storage>? Stores { get; set; } // Удалить.

    }
}
