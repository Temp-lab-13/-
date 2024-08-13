namespace WATask3.Client
{
    public interface ILibreryUsers
    {
        public Task<bool> Exist(Guid guid);
    }
}
