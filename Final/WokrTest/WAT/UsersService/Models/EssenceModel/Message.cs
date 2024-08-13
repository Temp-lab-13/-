namespace UsersService.Models.EssenceModel
{
    public class Message
    {
        public Guid? Id { get; set; }
        public Guid? ClientId { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
        public virtual User User{ get; set; }
    }
}
