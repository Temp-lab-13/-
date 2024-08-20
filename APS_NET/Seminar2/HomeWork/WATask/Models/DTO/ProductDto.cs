namespace WATask.Models.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Descript { get; set; }
        public int CategoriId { get; set; }
        public int? Price { get; set; }
    }
}
