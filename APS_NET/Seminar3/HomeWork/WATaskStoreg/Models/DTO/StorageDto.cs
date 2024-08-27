namespace WATaskStoreg.Models.DTO
{
    public class StorageDto
    {
        public int Id { get; set; }
        public int? productId { get; set; }
        public string? Name { get; set; }
        public string? Descript { get; set; }
        public int? Count { get; set; }
    }
}
