namespace WATask2.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Descript { get; set; }
        public int Count {  get; set; }
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
