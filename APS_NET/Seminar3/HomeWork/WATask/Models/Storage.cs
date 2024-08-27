namespace WATask.Models
{
    public class Storage : BModel
    {
        //  Сущность-апендикс. Позже переработаю сервис и выпилю остатки магазниа.
        public int Count {  get; set; }
        public virtual List<Product>? Products { get; set; }
    }
}
