namespace PochtaServers.Models.EssenceModel
{
    public class Client
    {
        public Guid? Id { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
