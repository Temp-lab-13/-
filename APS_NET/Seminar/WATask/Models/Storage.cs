namespace WATask.Models
{
    public class Storage : BModel
    {
        public int Count {  get; set; }
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
