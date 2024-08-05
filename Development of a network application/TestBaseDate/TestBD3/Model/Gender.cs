namespace TestBD3.Model
{
    public class Gender
    {
        public GenderId genderId {  get; set; }
        public string name { get; set; }
        public virtual List<User> users { get; set; }
    }
}