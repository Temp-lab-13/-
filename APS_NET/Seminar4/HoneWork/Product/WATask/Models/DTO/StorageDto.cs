namespace WATask.Models.DTO
{
    public class StorageDto
    {
        //  Сущность-апендикс. Позже переработаю сервис и выпилю остатки магазниа.
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Descript { get; set; }
        public int? Count { get; set; }
    }
}
