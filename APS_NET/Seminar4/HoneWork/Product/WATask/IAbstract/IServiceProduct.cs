using WATask.Models.DTO;

namespace WATask.IAbstract
{
    public interface IServiceProduct
    {
        bool AddProduct(ProductDto product);    // Методы буленвы потому то Граф требует у мутаций методов возращаяющий хоть что-то.
        IEnumerable<ProductDto> GetProducts();
        bool UpPrise(ProductDto product);
        bool DeletProduct(ProductDto product);
        bool CheckProduct(int productId);
        ProductDto GetProduct(int productId); 
    }
}
