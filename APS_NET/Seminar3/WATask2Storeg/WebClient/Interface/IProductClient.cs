namespace WATask2Storeg.WebClient.Interface
{
    public interface IProductClient
    {
        Task<bool> ExistProduct(int? productId);
    }
}
