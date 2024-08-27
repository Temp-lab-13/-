
namespace WATaskStoreg.WebClient.IAbstractClient
{
    public interface IStoregClient
    {
        Task<bool> ExistsProsuct(int id);
        //Task<ProductDto> GetProduct(int id);
    }
}
