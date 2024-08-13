namespace WATask3.Client
{
    public interface ILibreryClient
    {
        //void Connect(string username, string password);
        public Task<bool> Exist(Guid id);
    }
}
