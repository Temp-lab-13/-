namespace WATask1.DadaBase
{
    public class User
    {
        public Guid? Id { get; set; } // 8-4-4-4-12 формат id типа Guid
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime Registration {  get; set; } = DateTime.Now;
        public bool Active { get; set; } = true;
        
    }
}
