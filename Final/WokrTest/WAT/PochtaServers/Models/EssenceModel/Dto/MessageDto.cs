namespace PochtaServers.Models.EssenceModel.Dto
{
    public class MessageDto
    {
        public Guid? Id { get; set; }
        public Guid? ClientId { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
    }
}
