using WATask2.Models;

namespace WATask2Storeg.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public int? productId { get; set; }
        public string? Name { get; set; }
        public int? Count {  get; set; }
        public virtual List<Product>? Products { get; set; }
    }
}
